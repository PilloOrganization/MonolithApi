using Application.UnitOfWorks.Interfaces;
using Domain.Repositories.Interfaces;

namespace Infrastructure.EntityFramework.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlServerAppDbContext _context;

        public IUserRepository userRepository { get; set; }
        public IAccountRepository accountRepository { get; set; }
        public ICourseRepository courseRepository { get; set; }
        public IDoseRepository doseRepository { get; set; }
        public IMedicineRepository medicineRepository { get; set; }
        public IPrescriptionScheduleRepository prescriptionScheduleRepository { get; set; }

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

            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            this.doseRepository = doseRepository ?? throw new ArgumentNullException(nameof(doseRepository));
            this.medicineRepository = medicineRepository ?? throw new ArgumentNullException(nameof(medicineRepository));
            this.prescriptionScheduleRepository = prescriptionScheduleRepository ?? throw new ArgumentNullException(nameof(prescriptionScheduleRepository));
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
