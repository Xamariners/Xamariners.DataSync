using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamariners.DataSync.Common;
using Xamariners.DataSync.Common.Enum;
using Xamariners.DataSync.Common.Helper;
using Xamariners.DataSync.Common.Implementation;
using Xamariners.DataSync.Common.Infrastructure;
using Xamariners.DataSync.Common.Interface;
using Xamariners.DataSync.Common.Model;

namespace Xamariners.DataSync.Client
{
    public abstract class CacheUpdater : ICacheUpdater
    {
        private readonly IDataUpdateService _dataUpdateService;
        private readonly ICacheService _cacheService;
        private readonly object _lock = new object();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private Task _task;

        public CacheUpdater(IDataUpdateService dataUpdateService, ICacheService cacheService)
        {
            _dataUpdateService = dataUpdateService;
            _cacheService = cacheService;
        }

        public virtual void StartCacheUpdater(bool updateLocalRepository, int refreshRate, AppStatus appStatus, Guid dataUpdateTargetId, Type dataUpdateTargetType, DateTime cacheLastUpdated, object state)
        {
           
#if !FAKE
            MiscHelpers.ThrowIfNull(() => _dataUpdateService);

            _task = MiscHelpers.StartNewCancellableTask(() =>
                UpdateCache(updateLocalRepository, appStatus, dataUpdateTargetId, dataUpdateTargetType, cacheLastUpdated, state),
                () => MiscHelpers.ThrowIfNull(() => _dataUpdateService),
                _cancellationTokenSource,
                _lock, refreshRate);
#endif
        }

        public void StopCacheUpdater()
        {
#if !FAKE
            if (_cancellationTokenSource.IsCancellationRequested)
                return;

            _cancellationTokenSource.Cancel();

            if (_task == null)
                return;

            while (!_task.IsCanceled && !_task.IsCompleted)
            {
                Task.Delay(1000).Wait();
            }
#endif
        }

        protected virtual void ProcessUpdates(IEnumerable<DataUpdate> updates)
        {
            if (updates == null)
                return;
            
            foreach (DataUpdate update in updates)
            {
                var type = Type.GetType(update.Type);

                if (type == null)
                    throw new Exception($"Failed to resolve type: {update.Type}");

                if (type == typeof(CachedQuery))
                    Resolver.Instance.Resolve<ICacheService>().Cache.SaveCachedQuery(BinarySerialiser.DeSerializeObject<CachedQuery>((byte[])update.Payload));
                else if (ProcessUpdate(update, type))
                    continue;
                else
                {
                    var payload = (byte[])update.Payload;
                    var item = BinarySerialiser.DeSerializeObject((byte[])update.Payload, type);

                    if (payload.Length > 0 && item == null)
                        throw new Exception($"error ProcessUpdate for type {type.Name}");

                    SaveObjectToCache(item, type);
                }
            }

            if(!CheckCacheIntegrity());
                throw new Exception("Cache integrity is compromised");
        
        }

        public abstract bool ProcessUpdate(DataUpdate update, Type updateType);

        public abstract bool CheckCacheIntegrity();

        public DateTime UpdateCache(bool updateLocalRepository, AppStatus appStatus, Guid dataUpdateTargetId, Type dataUpdateTargetType, DateTime cacheLastUpdated, object state, bool forceUpdate = false)
        {
            try { 

                if (!forceUpdate && new[] { AppStatus.Running, AppStatus.JustStarted, AppStatus.JustWokeUp }.Contains(appStatus) == false)
                    return cacheLastUpdated;

                lock (_lock)
                {
                    // if we logged out, the cache would not be init
                    if (!Resolver.Instance.Resolve<ICacheService>().Cache.IsInitialised)
                        Resolver.Instance.Resolve<ICacheService>().Initialise();

                    if (updateLocalRepository)
                    {
                        DateTime? timestamp = null;

                        // Get and save data updates
                        timestamp = UpdateLocalRepository(dataUpdateTargetId, dataUpdateTargetType, cacheLastUpdated);

                        if (timestamp.HasValue)
                            cacheLastUpdated = timestamp.Value.ToUniversalTime();
                        else
                            throw new Exception("Could not get initial data load");
                    }

                    // save state
                    UpdateLocalState(state);

                    // force persist cache 
                    if (Resolver.Instance.Resolve<ICacheService>().Cache is InMemoryCache)
                        ((InMemoryCache)Resolver.Instance.Resolve<ICacheService>().Cache).ForcePersistCacheStateAsync();

                    return cacheLastUpdated;
                }
            }
            catch (Exception exception)
            {
               // TODO: log exception
                throw;
            }
        }

        private DateTime? UpdateLocalRepository(Guid DataUpdateTargetId, Type dataUpdateTargetType, DateTime cacheLastUpdated)
        {
            var response = _dataUpdateService.GetDataUpdates(DataUpdateTargetId, dataUpdateTargetType, cacheLastUpdated).Result;
           
            if (response.Data == null || response.Data.Length == 0)
                return response.ProcessingTimestamp;

            var updates = BinarySerialiser.DeSerializeObject<IList<DataUpdate>>(response.Data);

            ProcessUpdates(updates);

            var cache = Resolver.Instance.Resolve<ICacheService>().Cache;

            if (cache.CountCachedObjects() == 0)
            {
                return null;
            }

            return response.ProcessingTimestamp;
        }

        private void UpdateLocalState(object state)
        {
            Resolver.Instance.Resolve<ICacheService>().Cache.SaveObject(state, 0);
        }

        public bool IsRunning { get; private set; }


        public IEnumerable<T> GetCachedData<T>(Func<T, bool> filter = null, Func<IEnumerable<T>> fallback = null)
        {
            var cache = Resolver.Instance.Resolve<ICacheService>().Cache;
            var data = cache.GetAllData<T>();

            if (filter == null)
                return data;

            if (data == null)
                return fallback != null ? fallback() : new List<T>();

            var result = data.Where(filter);

            try
            {
                var cachedData = result.ToArray() ?? result as T[];

                if (cachedData.Any())
                    return cachedData;
            }
            catch (Exception ex)
            {
                //throw new Exception($"GetCachedData: cannot get results for type {typeof(T)}", ex);
            }

            return fallback != null ? fallback() : new List<T>();
        }

        protected void SaveObjectToCache(object payload, Type type)
        {
            ReflectionHelper.ExecuteGenericMethod(
                Resolver.Instance.Resolve<ICacheService>().Cache,
                typeof(ICache),
                type,
                "SaveObject",
                payload,
                (uint)0);
        }

        public void RemoveItem<T>(Guid id)
        {
            try
            {
                Resolver.Instance.Resolve<ICacheService>().Cache.DeleteObject<T>(id);
            }
            catch (Exception exception)
            {
                // log: $"Failed trying to remove object from the local cache. {typeof(T).Name}:{id}"
            }
        }

        public void StartUpdaters(bool updateLocalRepository, AppStatus appStatus, Guid dataUpdateTargetId, Type dataUpdateTargetType, DateTime cacheLastUpdated, object state, int refreshRate)
        {
            // get first update
            UpdateCache(updateLocalRepository, appStatus, dataUpdateTargetId, dataUpdateTargetType, cacheLastUpdated, state);

            // run cyclic updater
            StartCacheUpdater(updateLocalRepository, refreshRate, appStatus, dataUpdateTargetId, dataUpdateTargetType, cacheLastUpdated, state);
        }
    }
}
