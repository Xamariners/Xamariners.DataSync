using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.DataSync.Common.Model
{
    /// <summary>
    /// The cached object.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    //[PSerializable]
    [DataContract]
    public class CachedObject<T> : CacheableBase
    {
        public CachedObject(string cacheKey, T entity) : this()
        {
            Object = entity;
            CacheKey = cacheKey;
        }

        public CachedObject()
        {
            CacheCreated = DateTime.UtcNow;
        }

        [DataMember]
        public T Object { get; set; }
    }
}
