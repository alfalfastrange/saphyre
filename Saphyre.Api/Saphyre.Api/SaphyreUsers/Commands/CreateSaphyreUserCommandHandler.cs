using MediatR;
using Saphyre.Api.SaphyreUsers.Entities;
using Saphyre.Api.SaphyreUsers.Providers;
using Saphyre.Api.SaphyreUsers.Validation;
using Saphyre.Api.SaphyreUsers.ViewModels;

namespace Saphyre.Api.SaphyreUsers.Commands
{
    public static class CreateSaphyreUserCommandHandler
    {
        public class Command : IRequest<Response>
        {
            public Command(CreateOrUpdateSaphyreUserViewModel model)
            {
                Model = model;
            }

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
            private readonly ICreateSaphyreUserCommandValidator _validator;

            public Handler(
                ISaphyreUserProvider saphyreUserProvider,
                ICreateSaphyreUserCommandValidator validator)
            {
                _saphyreUserProvider = saphyreUserProvider;
                _validator = validator;
            }

            public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.Validate(command);

                if (!validationResult.IsValid)
                {
                    return new Response(validationResult.ErrorDetails.Select(x => x.ErrorMessage).ToList());
                }

                var user = new SaphyreUser
                {
                    FirstName = command.Model.FirstName,
                    LastName = command.Model.LastName,
                    DateOfBirth = command.Model.DateOfBirth
                };

                var isCreated = await _saphyreUserProvider.Create(user, cancellationToken);

                if (isCreated)
                {
                    return new Response();
                }
                else
                {
                    return new Response("Error creating user");
                }
            }
        }
    }
}
