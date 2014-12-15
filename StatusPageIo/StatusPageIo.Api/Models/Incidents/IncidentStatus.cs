using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusPageIo.Api.Models.Incidents
{
    public enum IncidentStatus
    {
        Investigating,
        Identified,
        Monitoring,
        Resolved,
        Scheduled,
        InProgress,
        Verifying,
        Completed
    }
}
