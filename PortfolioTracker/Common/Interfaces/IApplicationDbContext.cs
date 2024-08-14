using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Data;

namespace PortfolioTracker.Common.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        public DbSet<PlatformKeyData> ApiKeyData { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        // Are implemented by DbContext, needed here only so custom DbSet Properties can be saved to Db without casting interface to concrete implementation
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}