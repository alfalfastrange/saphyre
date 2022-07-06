using MediatR;
using Saphyre.Api.SaphyreUsers.Providers;

namespace Saphyre.Api.SaphyreUsers.Commands
{
    public static class DeleteSaphyreUserCommandHandler
    {
        public class Command : IRequest<Response>
        {
            public Command(int userId)
            {
                UserId = userId;
            }

            public int UserId { get; }
        }

        public class Response
        {
            public Response()
            {
                Errors = new List<string>();
            }

            public Response(string error)
            {
                Errors = new List<string>();
                Errors.Add(error);
            }

            public Response(List<string> errors)
            {
                Errors = errors;
            }

            public List<string> Errors { get; }

            public bool IsSuccess => !Errors.Any();
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly ISaphyreUserProvider _saphyreUserProvider;

            public Handler(ISaphyreUserProvider saphyreUserProvider)
            {
                _saphyreUserProvider = saphyreUserProvider;
            }

            public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await _saphyreUserProvider.GetById(command.UserId, cancellationToken);

                if (user == null)
                {
                    return new Response("User not found");
                }

                var isDeleted = await _saphyreUserProvider.Delete(user, cancellationToken);

                if (isDeleted)
                {
                    return new Response();
                }
                else
                {
                    return new Response("Error deleting user");
                }
            }
        }
    }
}
