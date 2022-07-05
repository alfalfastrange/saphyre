using Saphyre.Api.Infrastructure.IoC;

namespace Saphyre.Api.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddBindings(
            this IServiceCollection services,
            List<IBinder> binders)
        {
            binders.ForEach(x => x.Bind(services));
        }
    }
}
