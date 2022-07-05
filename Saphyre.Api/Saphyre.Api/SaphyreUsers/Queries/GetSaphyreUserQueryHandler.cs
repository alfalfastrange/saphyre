using MediatR;
using Saphyre.Api.SaphyreUsers.Extensions;
using Saphyre.Api.SaphyreUsers.Providers;
using Saphyre.Api.SaphyreUsers.ViewModels;

namespace Saphyre.Api.SaphyreUsers.Queries
{
    public static class GetSaphyreUserQueryHandler
    {
        public class Query : IRequest<Response>
        {
            public Query(int userId)
            {
                UserId = userId;
            }

            public int UserId { get; }
        }

        public class Response
        {
            public Response(SaphyreUserViewModel model)
            {
                Model = model;
            }

            public SaphyreUserViewModel Model { get; }
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
                var saphyreUser = await _saphyreUserProvider.GetById(query.UserId, cancellationToken);
                var model = saphyreUser.ToViewModel();
                return new Response(model);
            }
        }
    }
}
