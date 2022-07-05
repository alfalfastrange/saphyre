using Saphyre.Api.SaphyreUsers.Commands;
using Saphyre.Api.Shared.Validation;

namespace Saphyre.Api.SaphyreUsers.Validation
{
    public interface IUpdateSaphyreUserCommandValidator
    {
        Task<ValidationResultViewModel> Validate(UpdateSaphyreUserCommandHandler.Command command);
    }
}
