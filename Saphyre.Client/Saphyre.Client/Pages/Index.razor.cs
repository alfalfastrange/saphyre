using Microsoft.AspNetCore.Components;
using Saphyre.Client.Providers;
using Saphyre.Client.ViewModels;

namespace Saphyre.Client.Pages
{
    public partial class Index
    {
        private List<SaphyreUser> saphyreUsers { get; set; } = new List<SaphyreUser>();

        [Inject]
        private ISaphyreUsersProvider saphyreUsersProvider { get; set; }

        protected async override Task OnInitializedAsync()
        {
            saphyreUsers = await saphyreUsersProvider.GetSaphyreUsers();
        }

        private async Task DeleteSaphyreUser(SaphyreUser saphyreUser)
        {
            await saphyreUsersProvider.DeleteSaphyreUser(saphyreUser.UserId);
            saphyreUsers = await saphyreUsersProvider.GetSaphyreUsers();
        }
    }
}
