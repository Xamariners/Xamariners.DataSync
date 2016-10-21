using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.DataSync.Common.Enum;

namespace Xamariners.DataSync.Common.Interface
{
    public interface ICacheUpdater
    {
        void StartCacheUpdater(bool updateLocalRepository, int refreshRate, AppStatus appStatus, Guid dataUpdateTargetId,
            Type dataUpdateTargetType, DateTime cacheLastUpdated, object state);

        void StopCacheUpdater();

        DateTime UpdateCache(bool updateLocalRepository, AppStatus appStatus, Guid dataUpdateTargetId, Type dataUpdateTargetType,
            DateTime cacheLastUpdated, object state, bool forceUpdate = false);

        bool IsRunning { get; }

        IEnumerable<T> GetCachedData<T>(Func<T, bool> filter = null, Func<IEnumerable<T>> fallback = null);

        void RemoveItem<T>(Guid id);

        void StartUpdaters(bool updateLocalRepository, AppStatus appStatus, Guid dataUpdateTargetId,
            Type dataUpdateTargetType, DateTime cacheLastUpdated, object state, int refreshRate);
    }
}
