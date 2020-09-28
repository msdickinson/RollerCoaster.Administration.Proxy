using DickinsonBros.DurableRest.Abstractions;
using DickinsonBros.DurableRest.Abstractions.Models;
using Microsoft.Extensions.Options;
using RollerCoaster.Administration.Proxy.Models;
using RollerCoaster.Administration.Proxy.Models.TelemetryQuery;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RollerCoaster.Administration.Proxy
{
    public class AdministrationProxyService : IAdministrationProxyService
    {
        internal readonly AdministrationProxyOptions _administrationProxyOptions;
        internal readonly IDurableRestService _durableRestService;
        internal readonly HttpClient _httpClient;

        public const string AUTHORIZATION = "Authorization";

        public AdministrationProxyService(IDurableRestService durableRestService, HttpClient httpClient, IOptions<AdministrationProxyOptions> administrationProxyOptions)
        {
            _durableRestService = durableRestService;
            _administrationProxyOptions = administrationProxyOptions.Value;
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> LogAsync()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_administrationProxyOptions.Log.Resource, UriKind.Relative),
            };

            return await _durableRestService.ExecuteAsync(_httpClient, httpRequestMessage, _administrationProxyOptions.Log.Retrys, _administrationProxyOptions.Log.TimeoutInSeconds).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> AdminAuthorizedAsync(string bearerToken)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_administrationProxyOptions.AdminAuthorized.Resource, UriKind.Relative)
            };

            httpRequestMessage.Headers.Add(AUTHORIZATION, bearerToken);

            return await _durableRestService.ExecuteAsync(_httpClient, httpRequestMessage, _administrationProxyOptions.AdminAuthorized.Retrys, _administrationProxyOptions.AdminAuthorized.TimeoutInSeconds).ConfigureAwait(false);
        }


        public async Task<HttpResponse<List<TelemetryData>>> TelemetryQueryAsync(TelemetryQueryRequest request, string bearerToken)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_administrationProxyOptions.TelemetryQuery.Resource, UriKind.Relative),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            httpRequestMessage.Headers.Add(AUTHORIZATION, bearerToken);

            return await _durableRestService.ExecuteAsync<List<TelemetryData>>(_httpClient, httpRequestMessage, _administrationProxyOptions.TelemetryQuery.Retrys, _administrationProxyOptions.TelemetryQuery.TimeoutInSeconds).ConfigureAwait(false);
        }

    }

}
