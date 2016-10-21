using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xamariners.DataSync.Common.Interface;

namespace Xamariners.DataSync.Common.Model
{
    [DataContract]
    public abstract class CacheableBase : ICacheable
    {

        [DataMember]
        public DateTime CacheCreated { get; set; }

        [DataMember]
        public DateTime CacheRetrieved { get; set; }

        [DataMember]
        public DateTime CacheUpdated { get; set; }

        [DataMember]
        public string CacheKey { get; set; }

        [DataMember]
        public TimeSpan Expiry { get; set; }
    }
}
