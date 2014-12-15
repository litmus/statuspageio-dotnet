using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusPageIo.Api.Models.Metrics
{
    public enum  MetricProviderType
    {
        Pingdom,
        NewRelic,
        Librato,
        Datadog,
        Self
    }
}
