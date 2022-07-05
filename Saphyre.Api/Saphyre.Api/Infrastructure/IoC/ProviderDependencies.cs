using Saphyre.Api.SaphyreUsers.Providers;

namespace Saphyre.Api.Infrastructure.IoC
{
    public class ProviderDependencies : IBinder
    {
        public void Bind(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ISaphyreUserProvider, SaphyreUserProvider>();
        }
    }
}
