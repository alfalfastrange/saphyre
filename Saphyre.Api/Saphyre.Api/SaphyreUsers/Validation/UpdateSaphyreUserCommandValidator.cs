using Saphyre.Api.SaphyreUsers.Commands;
using Saphyre.Api.Shared.Validation;

namespace Saphyre.Api.SaphyreUsers.Validation
{
    public class UpdateSaphyreUserCommandValidator : BaseCommandValidator, IUpdateSaphyreUserCommandValidator
    {
        public async Task<ValidationResultViewModel> Validate(UpdateSaphyreUserCommandHandler.Command command)
        {
            Validate(command.Model.FirstName.MustNotBeNull(), ValidationTypeEnum.Required, "USR_UPD_001", "First name is required");
            Validate(command.Model.FirstName.MustHaveLengthAtLeast(2), ValidationTypeEnum.Length, "USR_UPD_002", "First name is too short, minimum of 2 characters");
            Validate(command.Model.FirstName.MustHaveLengthAtMost(50), ValidationTypeEnum.Length, "USR_UPD_003", "First name is too long, maximum of 50 characters");
            Validate(command.Model.LastName.MustNotBeNull(), ValidationTypeEnum.Required, "USR_UPD_004", "Last name is required");
            Validate(command.Model.LastName.MustHaveLengthAtLeast(2), ValidationTypeEnum.Length, "USR_UPD_005", "Last name is too short, minimum of 2 characters");
            Validate(command.Model.LastName.MustHaveLengthAtMost(50), ValidationTypeEnum.Length, "USR_UPD_006", "Last name is too long, maximum of 50 characters");
            Validate(command.Model.DateOfBirth.MustBeInPast(), ValidationTypeEnum.Date, "USR_UPD_007", "Date of birth must be in the past");

            return new ValidationResultViewModel(Errors.ToList());
        }
    }
}
