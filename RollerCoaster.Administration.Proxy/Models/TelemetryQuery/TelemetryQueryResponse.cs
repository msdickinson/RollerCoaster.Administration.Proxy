using DickinsonBros.Telemetry.Abstractions.Models;
using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Administration.Proxy.Models.TelemetryQuery
{
    [ExcludeFromCodeCoverage]
    public class TelemetryData
    {
        public string Name { get; set; }
        public TelemetryType TelemetryType { get; set; }
        public int ElapsedMilliseconds { get; set; }
        public TelemetryState TelemetryState { get; set; }
        public System.DateTime DateTime { get; set; }
        public string Source { get; set; }
    }
}
