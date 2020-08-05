using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusPageIo.Api.Models.Components
{
    public enum ComponentStatus
    {
        Operational, DegradedPerformance, PartialOutage, MajorOutage, UnderMaintenance
    }
}
