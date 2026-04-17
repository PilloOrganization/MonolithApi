using Application.UnitOfWorks.Interfaces;
using Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository userRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAccountRepository accountRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICourseRepository courseRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDoseRepository doseRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMedicineRepository medicineRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPrescriptionScheduleRepository prescriptionScheduleRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
