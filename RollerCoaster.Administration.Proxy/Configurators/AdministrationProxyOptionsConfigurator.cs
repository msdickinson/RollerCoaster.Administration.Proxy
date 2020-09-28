using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RollerCoaster.Administration.Proxy.Models;

namespace RollerCoaster.Account.Proxy.Configurators
{
    public class AdministrationProxyOptionssConfigurator : IConfigureOptions<AdministrationProxyOptions>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public AdministrationProxyOptionssConfigurator(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        void IConfigureOptions<AdministrationProxyOptions>.Configure(AdministrationProxyOptions options)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var configuration = provider.GetRequiredService<IConfiguration>();
                var accountProxyOptions = configuration.GetSection(nameof(AdministrationProxyOptions)).Get<AdministrationProxyOptions>();
                configuration.Bind($"{nameof(AdministrationProxyOptions)}", options);
            }
        }
    }
}
