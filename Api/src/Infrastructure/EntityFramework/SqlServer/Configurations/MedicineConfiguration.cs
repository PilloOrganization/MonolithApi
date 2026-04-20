using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.SqlServer.Configurations
{
    public class MedicineConfiguration : BaseEntityConfiguration<Medicine>
    {
        public override void Configure(EntityTypeBuilder<Medicine> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(m => m.Name)
                .IsUnique();
        }
    }
}
