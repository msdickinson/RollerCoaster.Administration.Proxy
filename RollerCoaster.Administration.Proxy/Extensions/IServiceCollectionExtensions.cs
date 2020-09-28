using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using RollerCoaster.Account.Proxy.Configurators;
using RollerCoaster.Administration.Proxy.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Administration.Proxy.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAdministrationProxyService(this IServiceCollection serviceCollection, Uri baseAddress, TimeSpan httpClientTimeout)
        {
            serviceCollection.AddHttpClient<IAdministrationProxyService, AdministrationProxyService>(client =>
            {
                client.BaseAddress = baseAddress;
                client.Timeout = httpClientTimeout;
            });

            serviceCollection.TryAddSingleton<IConfigureOptions<AdministrationProxyOptions>, AdministrationProxyOptionssConfigurator>();

            return serviceCollection;
        }
    }
}
