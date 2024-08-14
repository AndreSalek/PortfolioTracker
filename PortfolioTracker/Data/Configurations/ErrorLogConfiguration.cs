using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolioTracker.Data.Configurations
{
    public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Id)
                .UseIdentityColumn();
            // Converter not needed, because EF core has predefined conversions
            // https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#pre-defined-conversions

            builder.Property(prop => prop.LogLevel)
                .HasColumnType("nvarchar(10)")
                .IsRequired();
            builder.Property(prop => prop.EventId)
                .IsRequired();
            builder.Property(prop => prop.EventName)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(prop => prop.ExceptionMessage)
                .IsRequired();
            builder.Property(prop => prop.StackTrace)
                .IsRequired();
            builder.Property(prop => prop.Source)
                .IsRequired();
            builder.Property(prop => prop.DateCreated)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
