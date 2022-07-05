using Saphyre.Api.SaphyreUsers.Validation;

namespace Saphyre.Api.Infrastructure.IoC
{
    public class ValidationDependencies : IBinder
    {
        public void Bind(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICreateSaphyreUserCommandValidator, CreateSaphyreUserCommandValidator>();
            serviceCollection.AddScoped<IUpdateSaphyreUserCommandValidator, UpdateSaphyreUserCommandValidator>();
        }
    }
}
