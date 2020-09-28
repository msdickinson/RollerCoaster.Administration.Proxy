using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RollerCoaster.Account.Proxy.Configurators;
using RollerCoaster.Administration.Proxy.Extensions;
using RollerCoaster.Administration.Proxy.Models;
using System;
using System.Linq;

namespace RollerCoaster.Administration.Proxy.Tests.Extensions
{
    [TestClass]
    public class IServiceCollectionExtensionsTests
    {
        [TestMethod]
        public void AddDateTimeService_Should_Succeed()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var baseAddress = new Uri("https://Localhost:8080");
            var httpClientTimeout = new TimeSpan(0, 0, 30);

            // Act
            serviceCollection.AddAdministrationProxyService(baseAddress, httpClientTimeout);


            // Assert
            Assert.IsTrue(serviceCollection.Any(serviceDefinition => serviceDefinition.ServiceType == typeof(IAdministrationProxyService) &&
                                                       serviceDefinition.ImplementationFactory != null &&
                                                       serviceDefinition.Lifetime == ServiceLifetime.Transient));

            Assert.IsTrue(serviceCollection.Any(serviceDefinition => serviceDefinition.ServiceType == typeof(IConfigureOptions<AdministrationProxyOptions>) &&
                               serviceDefinition.ImplementationType == typeof(AdministrationProxyOptionssConfigurator) &&
                               serviceDefinition.Lifetime == ServiceLifetime.Singleton));
        }
    }
}