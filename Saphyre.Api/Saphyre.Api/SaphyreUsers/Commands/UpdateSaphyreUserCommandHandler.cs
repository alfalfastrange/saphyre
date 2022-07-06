using MediatR;
using Saphyre.Api.SaphyreUsers.Providers;
using Saphyre.Api.SaphyreUsers.Validation;
using Saphyre.Api.SaphyreUsers.ViewModels;

namespace Saphyre.Api.SaphyreUsers.Commands
{
    public static class UpdateSaphyreUserCommandHandler
    {
        public class Command : IRequest<Response>
        {
            public Command(
                int userId,
                CreateOrUpdateSaphyreUserViewModel model)
            {
                UserId = userId;
                Model = model;
            }

            public int UserId { get; }

            public CreateOrUpdateSaphyreUserViewModel Model { get; }
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
            private readonly IUpdateSaphyreUserCommandValidator _validator;

            public Handler(
                ISaphyreUserProvider saphyreUserProvider,
                IUpdateSaphyreUserCommandValidator validator)
            {
                _saphyreUserProvider = saphyreUserProvider;
                _validator = validator;
            }

            public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await _saphyreUserProvider.GetById(command.UserId, cancellationToken);

                if (user == null)
                {
                    return new Response("User not found");
                }

                var validationResult = await _validator.Validate(command);

                if (!validationResult.IsValid)
                {
                    return new Response(validationResult.ErrorDetails.Select(x => x.ErrorMessage).ToList());
                }

                user.Update(
                    command.Model.FirstName,
                    command.Model.LastName,
                    command.Model.DateOfBirth);
                var isUpdated = await _saphyreUserProvider.Update(user, cancellationToken);

                if (isUpdated)
                {
                    return new Response();
                }
                else
                {
                    return new Response("Error updating user");
                }
            }
        }
    }
}
