using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StatusPageIo.Api.Models.Metrics
{
    [DataContract]
    public class MetricProvider
    {
        [DataMember(Name = "required_fields")]
        public IList<string> RequiredFields { get; set; }

        [DataMember(Name = "type")]
        public MetricProviderType Type { get; set; }
    }
}
