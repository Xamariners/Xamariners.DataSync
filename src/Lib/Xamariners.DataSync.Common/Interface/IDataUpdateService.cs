using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.DataSync.Common.Model;

namespace Xamariners.DataSync.Common.Interface
{
    public interface IDataUpdateService
    {
        Task<DataUpdateResponse> GetDataUpdates(Guid targetId, Type targetType, DateTime timestamp);
    }
}