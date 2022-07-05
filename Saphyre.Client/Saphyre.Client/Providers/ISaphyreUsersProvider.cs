using Saphyre.Client.ViewModels;

namespace Saphyre.Client.Providers
{
    public interface ISaphyreUsersProvider
    {
        Task CreateSaphyreUser(SaphyreUser saphyreUser);

        Task<List<SaphyreUser>> GetSaphyreUsers();

        Task<SaphyreUser> GetSaphyreUser(string? userId);

        Task UpdateSaphyreUser(SaphyreUser saphyreUser);

        Task DeleteSaphyreUser(int userId);
    }
}
