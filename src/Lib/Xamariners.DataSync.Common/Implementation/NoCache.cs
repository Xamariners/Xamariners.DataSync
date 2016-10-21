using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.DataSync.Common.Model;

namespace Xamariners.DataSync.Common.Implementation
{
    // primarly used for unit tests
    /// <summary>
    ///     The no cache.
    /// </summary>
    //[Serializable]
    public class NoCache : CacheBase
    {
        #region Public Methods and Operators

        public override IQueryable<T> GetAllData<T>()
        {
            return null;
        }

        public override int CountCachedObjects()
        {
            return 0;
        }

        /// <summary>
        ///     The clear.
        /// </summary>
        public override void Clear()
        {
        }

        public override void SaveCachedQuery(CachedQuery cachedQuery)
        {

        }

        /// <summary>
        /// The delete query.
        /// </summary>
        /// <param name="cacheKey">
        /// The cache key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public override void DeleteQuery<T>(string cacheKey)
        {
        }

        public override void Initialise()
        {
        }

        public override void Shutdown()
        {
        }

        /// <summary>
        /// The get cached object.
        /// </summary>
        /// <param name="cacheKey">
        /// The cache key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="CachedObject"/>.
        /// </returns>
        public override CachedObject<T> GetCachedObject<T>(string cacheKey)
        {
            return null;
        }

        /// <summary>
        /// The get cached query.
        /// </summary>
        /// <param name="queryKey">
        /// The query key.
        /// </param>
        /// <returns>
        /// The <see cref="CachedQuery"/>.
        /// </returns>
        public override CachedQuery GetCachedQuery(string queryKey)
        {
            return null;
        }

        public override CachedObject<T> SaveObject<T>(T data, uint expiryInSeconds)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
