using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.DataSync.Common.Enum
{
    /// <summary>
    ///     The service status.
    /// </summary>
    public enum AppStatus
    {
        /// <summary>
        ///     The JustStarted.
        /// </summary>
        Running,

        /// <summary>
        ///     The JustStarted.
        /// </summary>
        JustStarted,

        /// <summary>
        ///     The error.
        /// </summary>
        JustWokeUp,

        /// <summary>
        ///     The GoingToSleep.
        /// </summary>
        GoingToSleep,

        /// <summary>
        ///     The Sleeping.
        /// </summary>
        Sleeping,
    }
}
