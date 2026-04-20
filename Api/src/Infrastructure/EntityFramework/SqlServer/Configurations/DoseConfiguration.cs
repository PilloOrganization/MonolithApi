using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.SqlServer.Configurations
{
    public class DoseConfiguration : BaseEntityConfiguration<Dose>
    {
        public override void Configure(EntityTypeBuilder<Dose> builder)
        {
            base.Configure(builder);

            builder.Property(d => d.Time)
                .IsRequired();

            builder.Property(d => d.IsTaken)
                .HasDefaultValue(false);

            builder.HasOne(d => d.PrescriptionSchedule)
                .WithMany(p => p.Doses)
                .HasForeignKey(d => d.PrescriptionScheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
