namespace Saphyre.Api.Shared.Validation
{
    public class ValidationDetailViewModel
    {
        public ValidationDetailViewModel(
            ValidationTypeEnum validationType,
            string errorCode,
            string errorMessage)
        {
            ValidationType = validationType;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public ValidationTypeEnum ValidationType { get; }

        public string ErrorCode { get; }

        public string ErrorMessage { get; }
    }
}
