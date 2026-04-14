using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IPrescriptionScheduleRepository
    {
        Task CreateAsync(PrescriptionSchedule prescriptionSchedule);
        Task DeleteAsync(Guid key);
        Task<IEnumerable<PrescriptionSchedule>> GetByCourseKeyAsync(Guid courseKey);
    }
}
