using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.DataSync.Common.Model
{/// <summary>
 /// The cache list.
 /// </summary>
    public class CachedQuery : CacheableBase
    {
        public CachedQuery()
        {

        }

        public CachedQuery(string cacheKey, List<string> itemCacheKeys = null)
        {
            CacheKey = cacheKey;// cacheKey.Item2;
            CacheCreated = DateTime.UtcNow;
            ItemsCacheKeys = itemCacheKeys;//new List<long>();
        }

        [DataMember]
        public List<string> ItemsCacheKeys { get; set; }
    }
}
