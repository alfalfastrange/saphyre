using Saphyre.Api.SaphyreUsers.Entities;

namespace Saphyre.Api.SaphyreUsers.Providers
{
    public interface ISaphyreUserProvider
    {
        Task<bool> Create(SaphyreUser saphyreUser, CancellationToken cancellationToken);

        Task<IEnumerable<SaphyreUser>> GetAll(CancellationToken cancellationToken);

        Task<SaphyreUser> GetById(int userId, CancellationToken cancellationToken);

        Task<bool> Update(SaphyreUser saphyreUser, CancellationToken cancellationToken);

        Task<bool> Delete(SaphyreUser saphyreUser, CancellationToken cancellationToken);
    }
}
