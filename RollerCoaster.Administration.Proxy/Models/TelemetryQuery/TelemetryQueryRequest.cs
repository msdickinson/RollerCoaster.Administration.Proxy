using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Administration.Proxy.Models.TelemetryQuery
{
    [ExcludeFromCodeCoverage]
    public class TelemetryQueryRequest
    {
        public string NameContains { get; set; }
        public List<int> TelemetryTypes { get; set; }
        public List<int> TelemetryStates { get; set; }
        public DateTime? StartDateTimeUTC { get; set; }
        public DateTime? EndDateTimeUTC { get; set; }
        public string SourceContains { get; set; }
    }
}
