using Microsoft.EntityFrameworkCore;
using Saphyre.Api.Infrastructure.Contexts;
using Saphyre.Api.SaphyreUsers.Entities;

namespace Saphyre.Api.SaphyreUsers.Providers
{
    public class SaphyreUserProvider : ISaphyreUserProvider
    {
        private readonly SaphyreContext _saphyreContext;

        public SaphyreUserProvider(SaphyreContext saphyreContext)
        {
            _saphyreContext = saphyreContext;
        }

        public async Task<bool> Create(SaphyreUser saphyreUser, CancellationToken cancellationToken)
        {
            await _saphyreContext.AddAsync(saphyreUser, cancellationToken);
            return await _saphyreContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<SaphyreUser>> GetAll(CancellationToken cancellationToken)
        {
            return await _saphyreContext.SaphyreUsers.ToListAsync(cancellationToken);
        }

        public async Task<SaphyreUser> GetById(int userId, CancellationToken cancellationToken)
        {
            var saphyreUser = await _saphyreContext.SaphyreUsers.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
            return saphyreUser;
        }

        public async Task<bool> Update(SaphyreUser saphyreUser, CancellationToken cancellationToken)
        {
            _saphyreContext.Update(saphyreUser);
            return await _saphyreContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Delete(SaphyreUser saphyreUser, CancellationToken cancellationToken)
        {
            _saphyreContext.Remove(saphyreUser);
            return await _saphyreContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
