using DickinsonBros.Test;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RollerCoaster.Account.Proxy.Configurators;
using RollerCoaster.Administration.Proxy.Models;
using System.Threading.Tasks;

namespace RollerCoaster.Administration.Proxy.Tests.Configurators
{

    [TestClass]
    public class AdministrationProxyOptionsConfiguratorTests : BaseTest
    {
        [TestMethod]
        public async Task Configure_Runs_ConfigReturns()
        {
            var administrationProxyOptions = new AdministrationProxyOptions
            {
                AdminAuthorized = new Models.ProxyOptions
                {
                    Resource = "SampleAdminAuthorizedResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                BaseURL = "SampleBaseURL",
                HttpClientTimeoutInSeconds = 1,
                Log = new Models.ProxyOptions
                {
                    Resource = "SampleLogResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                TelemetryQuery = new Models.ProxyOptions
                {
                    Resource = "SampleTelemetryQueryResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                }
            };

            var configurationRoot = BuildConfigurationRoot(administrationProxyOptions);

            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    //Act
                    var options = serviceProvider.GetRequiredService<IOptions<AdministrationProxyOptions>>().Value;

                    //Assert
                    Assert.IsNotNull(options);

                    Assert.AreEqual(administrationProxyOptions.AdminAuthorized.Resource             , options.AdminAuthorized.Resource);
                    Assert.AreEqual(administrationProxyOptions.AdminAuthorized.Retrys               , options.AdminAuthorized.Retrys);
                    Assert.AreEqual(administrationProxyOptions.AdminAuthorized.TimeoutInSeconds     , options.AdminAuthorized.TimeoutInSeconds);

                    Assert.AreEqual(administrationProxyOptions.Log.Resource                         , options.Log.Resource);
                    Assert.AreEqual(administrationProxyOptions.Log.Retrys                           , options.Log.Retrys);
                    Assert.AreEqual(administrationProxyOptions.Log.TimeoutInSeconds                 , options.Log.TimeoutInSeconds);

                    Assert.AreEqual(administrationProxyOptions.BaseURL                              , options.BaseURL);

                    Assert.AreEqual(administrationProxyOptions.HttpClientTimeoutInSeconds           , options.HttpClientTimeoutInSeconds);

                    Assert.AreEqual(administrationProxyOptions.TelemetryQuery.Resource              , options.TelemetryQuery.Resource);
                    Assert.AreEqual(administrationProxyOptions.TelemetryQuery.Retrys                , options.TelemetryQuery.Retrys);
                    Assert.AreEqual(administrationProxyOptions.TelemetryQuery.TimeoutInSeconds      , options.TelemetryQuery.TimeoutInSeconds);

                    await Task.CompletedTask.ConfigureAwait(false);

                },
                serviceCollection => ConfigureServices(serviceCollection, configurationRoot)
            );
        }

        #region Helpers

        private IServiceCollection ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<IConfigureOptions<AdministrationProxyOptions>, AdministrationProxyOptionssConfigurator>();

            return serviceCollection;
        }

        #endregion
    }
}
