using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.DataSync.Common.Model;

namespace Xamariners.DataSync.Common.Implementation
{
    public class InMemoryCache : CacheBase
    {
        public override void DeleteQuery<T>(string queryKey)
        {
            throw new NotImplementedException();
        }

        public override void Initialise()
        {
            throw new NotImplementedException();
        }

        public override void Shutdown()
        {
            throw new NotImplementedException();
        }

        public override IQueryable<T> GetAllData<T>()
        {
            throw new NotImplementedException();
        }

        public override int CountCachedObjects()
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override CachedObject<T> GetCachedObject<T>(string itemKey)
        {
            throw new NotImplementedException();
        }

        public override CachedQuery GetCachedQuery(string queryKey)
        {
            throw new NotImplementedException();
        }

        public override void SaveCachedQuery(CachedQuery cachedQuery)
        {
            throw new NotImplementedException();
        }

        public override CachedObject<T> SaveObject<T>(T data, uint expiryInSeconds)
        {
            throw new NotImplementedException();
        }

        public void ForcePersistCacheStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
