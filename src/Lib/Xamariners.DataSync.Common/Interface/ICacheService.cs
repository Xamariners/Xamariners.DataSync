using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.DataSync.Common.Interface
{
    /// <summary>
    ///     The CacheService interface.
    /// </summary>
    public interface ICacheService
    {
        #region Public Properties

        /// <summary>
        ///     Gets the cache.
        /// </summary>
        ICache Cache { get; }

        /// <summary>
        ///     Gets or sets the time to live.
        /// </summary>
        TimeSpan TimeToLive { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The initialise.
        /// </summary>
        void Initialise();

        #endregion
    }
}
