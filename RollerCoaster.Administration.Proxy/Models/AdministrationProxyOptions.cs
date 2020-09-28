using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Administration.Proxy.Models
{
    [ExcludeFromCodeCoverage]
    public class AdministrationProxyOptions
    {
        public string BaseURL { get; set; }
        public int HttpClientTimeoutInSeconds { get; set; }
        public ProxyOptions AdminAuthorized { get; set; }   
        public ProxyOptions Log { get; set; }
        public ProxyOptions TelemetryQuery { get; set; }
        
    }
}
