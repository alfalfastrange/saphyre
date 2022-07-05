using Microsoft.EntityFrameworkCore;
using Saphyre.Api.SaphyreUsers.Entities;

namespace Saphyre.Api.Infrastructure.Contexts
{
    public class SaphyreContext : DbContext
    {
        public SaphyreContext(DbContextOptions<SaphyreContext> options) : base(options) { }

        public DbSet<SaphyreUser> SaphyreUsers { get; set; }
    }
}
