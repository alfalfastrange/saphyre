using Saphyre.Api.SaphyreUsers.Entities;
using Saphyre.Api.SaphyreUsers.ViewModels;

namespace Saphyre.Api.SaphyreUsers.Extensions
{
    public static class SaphyreUserExtensions
    {
        public static SaphyreUserViewModel ToViewModel(this SaphyreUser saphyreUser)
        {
            return new SaphyreUserViewModel
            {
                UserId = saphyreUser.UserId,
                FirstName = saphyreUser.FirstName,
                LastName = saphyreUser.LastName,
                DateOfBirth = saphyreUser.DateOfBirth
            };
        }
    }
}
