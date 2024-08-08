using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace PortfolioTracker.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(prop => prop.ApiKeyList)
                .WithOne(prop => prop.User)
                .HasForeignKey(prop => prop.UserId);
        }
    }
}
