using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StatusPageIo.Api.Models.Metrics
{
    [DataContract]
    public class Data
    {
        [DataMember(Name = "timestamp")]
        public int Timestamp { get; set; }

        [DataMember(Name = "value")]
        public double Value { get; set; }
    }

    [DataContract]
    public class MetricData
    {
        [DataMember(Name = "data")]
        public Data Data { get; set; }
    }
}
