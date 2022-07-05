namespace Saphyre.Api.SaphyreUsers.ViewModels
{
    public class SaphyreUserViewModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FullName => string.Concat(FirstName, " ", LastName);
    }
}
