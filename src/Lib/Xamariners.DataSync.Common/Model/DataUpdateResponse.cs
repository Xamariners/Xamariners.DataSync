using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.DataSync.Common.Model
{
    public class DataUpdateResponse
    {
        public byte[] Data { get; set; }

        public DateTime ProcessingTimestamp { get; set; }

    }
}
