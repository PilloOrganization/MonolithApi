using Domain.Repositories.Interfaces;

namespace Application.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository userRepository { get; set; }
        IAccountRepository accountRepository { get; set; }
        ICourseRepository courseRepository { get; set; }
        IDoseRepository doseRepository { get; set; }
        IMedicineRepository medicineRepository { get; set; }
        IPrescriptionScheduleRepository prescriptionScheduleRepository { get; set; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
