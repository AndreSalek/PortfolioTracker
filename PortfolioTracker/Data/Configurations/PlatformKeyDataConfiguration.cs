using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioTracker.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioTracker.Data.Configurations
{
    public class PlatformKeyDataConfiguration : IEntityTypeConfiguration<PlatformKeyData>
    {
        public void Configure(EntityTypeBuilder<PlatformKeyData> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Platform)
                .HasMaxLength(64)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(prop => prop.ApiSecret)
                .IsRequired();
            builder.Property(prop => prop.Passphrase)
                .HasMaxLength(64);
            builder.HasOne(prop => prop.User)
                .WithMany(prop => prop.ApiKeyList)
                .HasForeignKey(prop => prop.Id);
        }
    }
}
