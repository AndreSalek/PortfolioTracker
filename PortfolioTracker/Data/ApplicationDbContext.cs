using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PortfolioTracker.Models;
using System.Reflection.Metadata;
using PortfolioTracker.Data.Configurations;
using Microsoft.AspNetCore.Identity;

namespace PortfolioTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<PlatformKeyData> ApiKeyData { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Renaming + custom model 
			builder.Entity<IdentityUser>().ToTable("Users");
			builder.Entity<User>().ToTable("Users");
			builder.Entity<IdentityRole>().ToTable("Role");
			builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
			builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
			builder.Entity< IdentityUserLogin< string>>().ToTable("UserLogins");
			builder.Entity<IdentityRoleClaim< string>>().ToTable("RoleClaims");
			builder.Entity<IdentityUserToken< string>>().ToTable("UserTokens");

			builder.ApplyConfiguration(new PlatformKeyDataConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
        }

    }
}
