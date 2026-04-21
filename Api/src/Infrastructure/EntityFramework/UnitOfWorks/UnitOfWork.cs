using Application.UnitOfWorks.Interfaces;
using Domain.Repositories.Interfaces;

namespace Infrastructure.EntityFramework.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlServerAppDbContext _context;

        public IUserRepository UserRepository { get; set; }
        public IAccountRepository AccountRepository { get; set; }
        public ICourseRepository CourseRepository { get; set; }
        public IDoseRepository DoseRepository { get; set; }
        public IMedicineRepository MedicineRepository { get; set; }
        public IPrescriptionScheduleRepository PrescriptionScheduleRepository { get; set; }

        public UnitOfWork(
            SqlServerAppDbContext context,
            IUserRepository userRepository,
            IAccountRepository accountRepository,
            ICourseRepository courseRepository,
            IDoseRepository doseRepository,
            IMedicineRepository medicineRepository,
            IPrescriptionScheduleRepository prescriptionScheduleRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            this.UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            this.CourseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            this.DoseRepository = doseRepository ?? throw new ArgumentNullException(nameof(doseRepository));
            this.MedicineRepository = medicineRepository ?? throw new ArgumentNullException(nameof(medicineRepository));
            this.PrescriptionScheduleRepository = prescriptionScheduleRepository ?? throw new ArgumentNullException(nameof(prescriptionScheduleRepository));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
