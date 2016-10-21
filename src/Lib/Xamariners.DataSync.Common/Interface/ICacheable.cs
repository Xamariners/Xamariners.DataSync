using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.DataSync.Common.Interface
{
    /// <summary>
    /// The Cacheable interface.
    /// </summary>
    public interface ICacheable
    {
        #region Public Properties
        /// <summary>
        ///     Gets or sets the cache created.
        /// </summary>
        [DataMember]
        DateTime CacheCreated { get; set; }

        /// <summary>
        ///     the cacke key, hashed from the cachekeystring
        /// </summary>
        //[Text]
        //string CacheKey { get; set; }

        /// <summary>
        ///     Gets or sets the cache retrieved.
        /// </summary>
        [DataMember]
        DateTime CacheRetrieved { get; set; }

        /// <summary>
        ///     Gets or sets the cache updated.
        /// </summary>
        [DataMember]
        DateTime CacheUpdated { get; set; }

        #endregion
    }
}
