using Domain.Repositories.Interfaces;

namespace Application.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; set; }
        IAccountRepository AccountRepository { get; set; }
        ICourseRepository CourseRepository { get; set; }
        IDoseRepository DoseRepository { get; set; }
        IMedicineRepository MedicineRepository { get; set; }
        IPrescriptionScheduleRepository PrescriptionScheduleRepository { get; set; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
