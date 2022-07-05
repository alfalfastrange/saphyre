namespace Saphyre.Api.SaphyreUsers.ViewModels
{
    public class SaphyreUsersViewModel
    {
        public SaphyreUsersViewModel(List<SaphyreUserViewModel> saphyreUsers)
        {
            SaphyreUsers = saphyreUsers;
        }

        public IEnumerable<SaphyreUserViewModel> SaphyreUsers { get; }
    }
}
