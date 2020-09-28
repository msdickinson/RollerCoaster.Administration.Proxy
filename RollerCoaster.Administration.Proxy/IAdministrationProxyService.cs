using DickinsonBros.DurableRest.Abstractions.Models;
using RollerCoaster.Administration.Proxy.Models.TelemetryQuery;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RollerCoaster.Administration.Proxy
{
    public interface IAdministrationProxyService
    {
        Task<HttpResponseMessage> AdminAuthorizedAsync(string bearerToken);
        Task<HttpResponseMessage> LogAsync();
        Task<HttpResponse<List<TelemetryData>>> TelemetryQueryAsync(TelemetryQueryRequest telemetryQueryRequest, string bearerToken);
    }
}