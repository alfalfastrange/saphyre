using Saphyre.Api.SaphyreUsers.Commands;
using Saphyre.Api.Shared.Validation;

namespace Saphyre.Api.SaphyreUsers.Validation
{
    public interface ICreateSaphyreUserCommandValidator
    {
        Task<ValidationResultViewModel> Validate(CreateSaphyreUserCommandHandler.Command command);
    }
}
