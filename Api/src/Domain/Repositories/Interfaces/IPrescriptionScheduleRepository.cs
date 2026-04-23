using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IPrescriptionScheduleRepository
    {
        Task<List<PrescriptionSchedule>> GetByCourseIdAsync(long courseId);
        Task<List<PrescriptionSchedule>> GetByCourseKeyAsync(Guid courseKey);
        Task<PrescriptionSchedule> GetAsync(Guid courseKey);
        void Create(PrescriptionSchedule prescriptionSchedule);
        void Delete(PrescriptionSchedule prescriptionSchedule);
    }
}
