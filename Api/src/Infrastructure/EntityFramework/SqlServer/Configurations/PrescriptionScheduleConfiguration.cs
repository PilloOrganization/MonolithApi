using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.SqlServer.Configurations
{
    public class PrescriptionScheduleConfiguration : BaseEntityConfiguration<PrescriptionSchedule>
    {
        public override void Configure(EntityTypeBuilder<PrescriptionSchedule> builder)
        {
            base.Configure(builder);

            builder.HasOne(p => p.Course)
                .WithMany(c => c.PrescriptionSchedules)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Medicine)
                .WithMany()
                .HasForeignKey(p => p.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
