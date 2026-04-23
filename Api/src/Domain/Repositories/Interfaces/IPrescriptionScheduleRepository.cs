using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IPrescriptionScheduleRepository
    {
        Task<List<PrescriptionSchedule>> GetByCourseIdAsync(long courseId);
        Task<List<PrescriptionSchedule>> GetByCourseKeyAsync(Guid courseKey);
        void Create(PrescriptionSchedule prescriptionSchedule);
        Task DeleteAsync(Guid key);
    }
}
