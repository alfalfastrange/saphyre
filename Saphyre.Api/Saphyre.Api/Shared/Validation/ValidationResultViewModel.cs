namespace Saphyre.Api.Shared.Validation
{
    public class ValidationResultViewModel
    {
        public ValidationResultViewModel(List<ValidationDetailViewModel> details)
        {
            Errors = new List<string>();
            ErrorTypes = new HashSet<ValidationTypeEnum>();
            foreach (var detail in details)
            {
                Errors.Add(detail.ErrorMessage);
                ErrorTypes.Add(detail.ValidationType);
            }
            ErrorDetails = details;
        }

        public bool IsValid => !Errors.Any();

        public List<string> Errors { get; }

        public HashSet<ValidationTypeEnum> ErrorTypes { get; }

        public List<ValidationDetailViewModel> ErrorDetails { get; }
    }
}
