using Domain.Models;
using Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.Repositories
{
    public class DoseRepository : IDoseRepository
    {
        public Task BulkCreateAsync(IEnumerable<Dose> doses)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task UpdateIsTakenAsync(Guid key, bool isTaken)
        {
            throw new NotImplementedException();
        }
    }
}
