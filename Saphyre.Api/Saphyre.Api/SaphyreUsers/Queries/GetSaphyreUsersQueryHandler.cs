using MediatR;
using Saphyre.Api.SaphyreUsers.Extensions;
using Saphyre.Api.SaphyreUsers.Providers;
using Saphyre.Api.SaphyreUsers.ViewModels;

namespace Saphyre.Api.SaphyreUsers.Queries
{
    public static class GetSaphyreUsersQueryHandler
    {
        public class Query : IRequest<Response> { }

        public class Response
        {
            public Response(SaphyreUsersViewModel model)
            {
                Model = model;
            }

            public SaphyreUsersViewModel Model { get; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ISaphyreUserProvider _saphyreUserProvider;

            public Handler(ISaphyreUserProvider saphyreUserProvider)
            {
                _saphyreUserProvider = saphyreUserProvider;
            }

            public async Task<Response> Handle(Query query, CancellationToken cancellationToken)
            {
                var saphyreUsers = await _saphyreUserProvider.GetAll(cancellationToken);
                var models = saphyreUsers.ToList().ConvertAll(x => x.ToViewModel());
                return new Response(new SaphyreUsersViewModel(models));
            }
        }
    }
}
