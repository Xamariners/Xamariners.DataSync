using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.DataSync.Common.Interface;
using Xamariners.DataSync.Common.Model;

namespace Xamariners.DataSync.Common.Implementation
{
    public abstract class CacheBase : ICache
    {
        public string LastCachedQueryKey { get; set; }

        public string LastCachedObjectKey { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsInitialised { get; set; }


        public abstract void Initialise();

        public abstract void Shutdown();

        public abstract IQueryable<T> GetAllData<T>();

        public abstract int CountCachedObjects();

        public abstract void Clear();

        public abstract CachedObject<T> GetCachedObject<T>(string itemKey);

        public abstract CachedQuery GetCachedQuery(string queryKey);

        public abstract CachedObject<T> SaveObject<T>(T data, uint expiryInSeconds) where T : new();

        public virtual IEnumerable<CachedObject<T>> SaveObjects<T>(IEnumerable<T> data, uint expiryInSeconds) where T : new()
        {
            var cachedObjects = new List<CachedObject<T>>();

            foreach (T item in data)
            {
                var cachedObject = SaveObject(item, expiryInSeconds);

                if (cachedObject != null)
                    cachedObjects.Add(cachedObject);
            }

            return cachedObjects;
        }

        public abstract void SaveCachedQuery(CachedQuery cachedQuery);

        public virtual CachedQuery SaveQuery<T>(IEnumerable<T> data, string queryKey, uint expiryInSeconds) where T : new()
        {
            LastCachedQueryKey = queryKey;
            return null;
        }

        public virtual CachedQuery SaveQuerySingle<T>(T data, string queryKey, uint expiryInSeconds) where T : new()
        {
            return SaveQuery(new[] { data }, queryKey, expiryInSeconds);
        }

        public abstract void DeleteQuery<T>(string queryKey);


        public CachedObject<T> PeekCachedObject<T>(string cacheKey)
        {
            return GetCachedObject<T>(cacheKey);
        }

        public CachedQuery PeekCachedQuery(string cacheKey)
        {
            return GetCachedQuery(cacheKey);
        }

        public virtual IEnumerable<CachedQuery> GetQueriesByItemId(string itemKey)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteObject<T>(Guid id)
        {

        }

        public virtual IEnumerable<CachedObject<T>> ResolveCachedQuery<T>(string queryKey)
        {
            try
            {
                var query = GetCachedQuery(queryKey);

                if (query == null || query.ItemsCacheKeys == null)
                    return null;

                var result = new List<CachedObject<T>>();

                foreach (string itemKey in query.ItemsCacheKeys)
                {
                    var cachedObject = GetCachedObject<T>(itemKey);
                    if (cachedObject != null)
                        result.Add(cachedObject);
                }

                return result;
            }
            catch (Exception)
            {
                //TODO: log exception
                return null;
            }
        }

        public CachedObject<T> ResolveCachedQuerySingle<T>(string queryKey)
        {
            try
            {
                var items = ResolveCachedQuery<T>(queryKey);
                return items != null ? items.SingleOrDefault() : null;
            }
            catch (Exception)
            {
                //TODO: log exception
                return null;
            }
        }

    }
}
