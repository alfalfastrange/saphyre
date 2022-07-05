using Microsoft.AspNetCore.Components;
using Saphyre.Client.Providers;
using Saphyre.Client.ViewModels;

namespace Saphyre.Client.Pages
{
    public partial class EditUser
    {
        [Parameter]
        public string? UserId { get; set; }

        private SaphyreUser saphyreUser { get; set; }

        [Inject]
        private ISaphyreUsersProvider saphyreUsersProvider { get; set; }


        protected async override Task OnInitializedAsync()
        {
            saphyreUser = await saphyreUsersProvider.GetSaphyreUser(UserId);
        }

        private async Task UpdateSaphyreUser()
        {
            await saphyreUsersProvider.UpdateSaphyreUser(saphyreUser);
            UriHelper.NavigateTo("/");
        }
    }
}
