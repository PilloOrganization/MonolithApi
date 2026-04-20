using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IPrescriptionScheduleRepository
    {
        Task<IEnumerable<PrescriptionSchedule>> GetByCourseIdAsync(long courseId);
        Task<IEnumerable<PrescriptionSchedule>> GetByCourseKeyAsync(Guid courseKey);
        void Create(PrescriptionSchedule prescriptionSchedule);
        Task DeleteAsync(Guid key);
    }
}
