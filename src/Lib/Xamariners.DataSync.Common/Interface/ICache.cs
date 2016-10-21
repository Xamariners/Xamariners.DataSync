using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.DataSync.Common.Model;

namespace Xamariners.DataSync.Common.Interface
{
    /// <summary>
    /// The Cache interface.
    /// </summary>
    public interface ICache
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the last list cache key.
        /// </summary>
        string LastCachedQueryKey { get; set; }

        /// <summary>
        ///     Gets or sets the last object cache key.
        /// </summary>
        string LastCachedObjectKey { get; set; }

        //bool IsDisabled { get; set; }
        bool IsInitialised { get; set; }

        #endregion

        IQueryable<T> GetAllData<T>();

        int CountCachedObjects();

        #region Public Methods and Operators

        /// <summary>
        /// The clear.
        /// </summary>
        void Clear();

        /// <summary>
        /// The retrieve.
        /// </summary>
        /// <param name="itemKey">
        /// The cache key.
        /// </param>
        /// <param name="returnType">
        /// The return type.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="ICacheable"/>.
        /// </returns>
        CachedObject<T> GetCachedObject<T>(string itemKey);

        IEnumerable<CachedObject<T>> ResolveCachedQuery<T>(string queryKey);

        CachedObject<T> ResolveCachedQuerySingle<T>(string queryKey);

        CachedQuery GetCachedQuery(string queryKey);

        void SaveCachedQuery(CachedQuery cachedQuery);

        /// <summary>
        /// The save list.
        /// </summary>
        /// <param name="queryKey">
        /// The cache key.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        CachedQuery SaveQuery<T>(IEnumerable<T> data, string queryKey, uint expiryInSeconds) where T : new();

        CachedObject<T> SaveObject<T>(T data, uint expiryInSeconds) where T : new();
        IEnumerable<CachedObject<T>> SaveObjects<T>(IEnumerable<T> data, uint expiryInSeconds) where T : new();

        void DeleteQuery<T>(string queryKey);

        void Initialise();

        void Shutdown();

        #endregion

        CachedObject<T> PeekCachedObject<T>(string toString);
        CachedQuery PeekCachedQuery(string lastCachedQueryKey);

        IEnumerable<CachedQuery> GetQueriesByItemId(string itemKey);

        void DeleteObject<T>(Guid id);
    }
}
