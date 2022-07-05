namespace Saphyre.Api.Shared.Validation
{
    public abstract class BaseCommandValidator
    {
        protected readonly ValidationResultViewModel Result;
        protected readonly HashSet<ValidationDetailViewModel> Errors;

        public BaseCommandValidator()
        {
            Errors = new HashSet<ValidationDetailViewModel>();
        }

        protected bool Validate(bool passes,
                                ValidationTypeEnum validationType,
                                string errorCode,
                                string errorMessage)
        {
            if (!passes)
            {
                Errors.Add(new ValidationDetailViewModel(validationType, errorCode, errorMessage));
            }
            return passes;
        }
    }
}
