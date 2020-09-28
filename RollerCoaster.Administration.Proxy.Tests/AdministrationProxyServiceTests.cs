using DickinsonBros.DurableRest.Abstractions;
using DickinsonBros.DurableRest.Abstractions.Models;
using DickinsonBros.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RollerCoaster.Administration.Proxy.Models;
using RollerCoaster.Administration.Proxy.Models.TelemetryQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RollerCoaster.Administration.Proxy.Tests
{
    [TestClass]
    public class AdministrationProxyServiceTests : BaseTest
    {
        const string BASE_URL = "https://localhost8080";
        const int HTTP_CLIENT_TIMEOUT_IN_SECONDS = 30;

        //LogProxyOptions
        const string LOG_PROXY_OPTION_RESOURCE = "SampleLogResource";
        const int LOG_PROXY_OPTION_RETRYS = 3;
        const double LOG_PROXY_OPTION_TIMEOUT_IN_SECONDS = 3;

        //AdminAuthorizedOptions
        const string ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE = "SampleAdminAuthorizedResource";
        const int ADMIN_AUTHORIZED_PROXY_OPTION_RETRYS = 6;
        const double ADMIN_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS = 6;

        //TelemetryQueryOptions
        const string TELEMETRY_QUERY_PROXY_OPTION_RESOURCE = "TelemetryQueryResource";
        const int TELEMETRY_QUERY_PROXY_OPTION_RETRYS = 6;
        const double TELEMETRY_QUERY_PROXY_OPTION_TIMEOUT_IN_SECONDS = 6;

        #region AdminAuthorizedAsync

        [TestMethod]
        public async Task AdminAuthorizedAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var bearerToken = "SampleBearerToken";
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
                            (
                                It.IsAny<HttpClient>(),
                                It.IsAny<HttpRequestMessage>(),
                                It.IsAny<int>(),
                                It.IsAny<double>()
                            )
                        )
                        .Callback((HttpClient httpClient, HttpRequestMessage httpRequestMessage, int retrys, double timeoutInSeconds) =>
                        {
                            observedHttpClient = httpClient;
                            observedHttpRequestMessage = httpRequestMessage;
                            observedRetrys = retrys;
                            observedTimeoutInSeconds = timeoutInSeconds;
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAdministrationProxyService>();
                    var uutConcrete = (AdministrationProxyService)uut;

                    //Act
                    var observed = await uut.AdminAuthorizedAsync(bearerToken);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync
                        (
                            It.IsAny<HttpClient>(),
                            It.IsAny<HttpRequestMessage>(),
                            It.IsAny<int>(),
                            It.IsAny<double>()
                        ),
                        Times.Once
                    );

                    Assert.AreEqual(uutConcrete._httpClient, observedHttpClient);
                    Assert.AreEqual(expectedMethod, observedHttpRequestMessage.Method);
                    Assert.IsTrue(observedHttpRequestMessage.Headers.First(e => e.Key == AdministrationProxyService.AUTHORIZATION).Value.First() == bearerToken);
                    Assert.AreEqual(expectedRequestUri.OriginalString, observedHttpRequestMessage.RequestUri.OriginalString);
                    Assert.IsNull(observedHttpRequestMessage.Content);
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._administrationProxyOptions.AdminAuthorized.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._administrationProxyOptions.AdminAuthorized.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task AdminAuthorizedAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var bearerToken = "SampleBearerToken";
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
                            (
                                It.IsAny<HttpClient>(),
                                It.IsAny<HttpRequestMessage>(),
                                It.IsAny<int>(),
                                It.IsAny<double>()
                            )
                        )
                        .Callback((HttpClient httpClient, HttpRequestMessage httpRequestMessage, int retrys, double timeoutInSeconds) =>
                        {
                            observedHttpClient = httpClient;
                            observedHttpRequestMessage = httpRequestMessage;
                            observedRetrys = retrys;
                            observedTimeoutInSeconds = timeoutInSeconds;
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAdministrationProxyService>();
                    var uutConcrete = (AdministrationProxyService)uut;

                    //Act
                    var observed = await uut.AdminAuthorizedAsync(bearerToken);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion

        #region LogAsync

        [TestMethod]
        public async Task LogAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{LOG_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
                            (
                                It.IsAny<HttpClient>(),
                                It.IsAny<HttpRequestMessage>(),
                                It.IsAny<int>(),
                                It.IsAny<double>()
                            )
                        )
                        .Callback((HttpClient httpClient, HttpRequestMessage httpRequestMessage, int retrys, double timeoutInSeconds) =>
                        {
                            observedHttpClient = httpClient;
                            observedHttpRequestMessage = httpRequestMessage;
                            observedRetrys = retrys;
                            observedTimeoutInSeconds = timeoutInSeconds;
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAdministrationProxyService>();
                    var uutConcrete = (AdministrationProxyService)uut;

                    //Act
                    var observed = await uut.LogAsync();

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync
                        (
                            It.IsAny<HttpClient>(),
                            It.IsAny<HttpRequestMessage>(),
                            It.IsAny<int>(),
                            It.IsAny<double>()
                        ),
                        Times.Once
                    );

                    Assert.AreEqual(uutConcrete._httpClient, observedHttpClient);
                    Assert.AreEqual(expectedMethod, observedHttpRequestMessage.Method);
                    Assert.AreEqual(expectedRequestUri.OriginalString, observedHttpRequestMessage.RequestUri.OriginalString);
                    Assert.IsNull(observedHttpRequestMessage.Content);
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._administrationProxyOptions.Log.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._administrationProxyOptions.Log.TimeoutInSeconds, (double)observedTimeoutInSeconds);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task LogAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{LOG_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
                            (
                                It.IsAny<HttpClient>(),
                                It.IsAny<HttpRequestMessage>(),
                                It.IsAny<int>(),
                                It.IsAny<double>()
                            )
                        )
                        .Callback((HttpClient httpClient, HttpRequestMessage httpRequestMessage, int retrys, double timeoutInSeconds) =>
                        {
                            observedHttpClient = httpClient;
                            observedHttpRequestMessage = httpRequestMessage;
                            observedRetrys = retrys;
                            observedTimeoutInSeconds = timeoutInSeconds;
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAdministrationProxyService>();
                    var uutConcrete = (AdministrationProxyService)uut;

                    //Act
                    var observed = await uut.LogAsync();

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion

        #region TelemetryQueryAsync

        [TestMethod]
        public async Task TelemetryQueryAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var telemetryQueryRequest = new TelemetryQueryRequest();
                    var httpResponse = new HttpResponse<List<TelemetryData>>();
                    var bearerToken = "SampleBearerToken";

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{TELEMETRY_QUERY_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(telemetryQueryRequest), Encoding.UTF8, "application/json");

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<List<TelemetryData>>
                            (
                                It.IsAny<HttpClient>(),
                                It.IsAny<HttpRequestMessage>(),
                                It.IsAny<int>(),
                                It.IsAny<double>()
                            )
                        )
                        .Callback((HttpClient httpClient, HttpRequestMessage httpRequestMessage, int retrys, double timeoutInSeconds) =>
                        {
                            observedHttpClient = httpClient;
                            observedHttpRequestMessage = httpRequestMessage;
                            observedRetrys = retrys;
                            observedTimeoutInSeconds = timeoutInSeconds;
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAdministrationProxyService>();
                    var uutConcrete = (AdministrationProxyService)uut;

                    //Act
                    var observed = await uut.TelemetryQueryAsync(telemetryQueryRequest, bearerToken);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync<List<TelemetryData>>
                        (
                            It.IsAny<HttpClient>(),
                            It.IsAny<HttpRequestMessage>(),
                            It.IsAny<int>(),
                            It.IsAny<double>()
                        ),
                        Times.Once
                    );

                    Assert.AreEqual(uutConcrete._httpClient, observedHttpClient);
                    Assert.AreEqual(expectedMethod, observedHttpRequestMessage.Method);
                    Assert.IsTrue(observedHttpRequestMessage.Headers.First(e => e.Key == AdministrationProxyService.AUTHORIZATION).Value.First() == bearerToken);
                    Assert.AreEqual(expectedRequestUri.OriginalString, observedHttpRequestMessage.RequestUri.OriginalString);
                    Assert.AreEqual(expectedContent.ToString(), observedHttpRequestMessage.Content.ToString());
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._administrationProxyOptions.TelemetryQuery.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._administrationProxyOptions.TelemetryQuery.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task TelemetryQueryAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var telemetryQueryRequest = new TelemetryQueryRequest();
                    var httpResponse = new HttpResponse<List<TelemetryData>>();
                    var bearerToken = "SampleBearerToken";

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{TELEMETRY_QUERY_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(telemetryQueryRequest), Encoding.UTF8, "application/json");

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<List<TelemetryData>>
                            (
                                It.IsAny<HttpClient>(),
                                It.IsAny<HttpRequestMessage>(),
                                It.IsAny<int>(),
                                It.IsAny<double>()
                            )
                        )
                        .Callback((HttpClient httpClient, HttpRequestMessage httpRequestMessage, int retrys, double timeoutInSeconds) =>
                        {
                            observedHttpClient = httpClient;
                            observedHttpRequestMessage = httpRequestMessage;
                            observedRetrys = retrys;
                            observedTimeoutInSeconds = timeoutInSeconds;
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAdministrationProxyService>();
                    var uutConcrete = (AdministrationProxyService)uut;

                    //Act
                    var observed = await uut.TelemetryQueryAsync(telemetryQueryRequest, bearerToken);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync<List<TelemetryData>>
                        (
                            It.IsAny<HttpClient>(),
                            It.IsAny<HttpRequestMessage>(),
                            It.IsAny<int>(),
                            It.IsAny<double>()
                        ),
                        Times.Once
                    );

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion
        #region Helpers

        private IServiceCollection ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAdministrationProxyService, AdministrationProxyService>();
            serviceCollection.AddSingleton(Mock.Of<IDurableRestService>());
            serviceCollection.AddSingleton(Mock.Of<HttpClient>());
            serviceCollection.AddOptions<AdministrationProxyOptions>()
                .Configure((administrationProxyOptions) =>
                {
                    administrationProxyOptions.BaseURL = BASE_URL;
                    administrationProxyOptions.HttpClientTimeoutInSeconds = HTTP_CLIENT_TIMEOUT_IN_SECONDS;

                    administrationProxyOptions.Log = new ProxyOptions
                    {
                        Resource = LOG_PROXY_OPTION_RESOURCE,
                        Retrys = LOG_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = LOG_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };

                    administrationProxyOptions.AdminAuthorized = new ProxyOptions
                    {
                        Resource = ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE,
                        Retrys = ADMIN_AUTHORIZED_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = ADMIN_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };

                    administrationProxyOptions.TelemetryQuery = new ProxyOptions
                    {
                        Resource = TELEMETRY_QUERY_PROXY_OPTION_RESOURCE,
                        Retrys = TELEMETRY_QUERY_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = TELEMETRY_QUERY_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                });

            return serviceCollection;
        }
        #endregion
    }
}
