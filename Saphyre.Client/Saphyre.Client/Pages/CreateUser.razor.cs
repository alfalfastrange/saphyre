using Microsoft.AspNetCore.Components;
using Saphyre.Client.Providers;
using Saphyre.Client.ViewModels;

namespace Saphyre.Client.Pages
{
    public partial class CreateUser
    {
        private SaphyreUser saphyreUser { get; set; } = new SaphyreUser();

        [Inject]
        private ISaphyreUsersProvider saphyreUsersProvider { get; set; }

        private async Task CreateSaphyreUser()
        {
            await saphyreUsersProvider.CreateSaphyreUser(saphyreUser);
            UriHelper.NavigateTo("/");
        }
    }
}
