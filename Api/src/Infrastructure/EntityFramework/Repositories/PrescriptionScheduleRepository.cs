using Domain.Models;
using Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.Repositories
{
    public class PrescriptionScheduleRepository : IPrescriptionScheduleRepository
    {
        public Task CreateAsync(PrescriptionSchedule prescriptionSchedule)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PrescriptionSchedule>> GetByCourseKeyAsync(Guid courseKey)
        {
            throw new NotImplementedException();
        }
    }
}
