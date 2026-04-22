using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class DoseRepository : IDoseRepository
    {
        private readonly SqlServerAppDbContext _context;

        public DoseRepository(SqlServerAppDbContext context)
        {
            _context = context;
        }

        public void BulkCreate(IEnumerable<Dose> doses)
        {
            _context.Doses.AddRange(doses);
        }

        public async Task UpdateIsTakenAsync(Guid key, bool isTaken)
        {
            await _context.Doses.Where(e => e.EntityKey == key).ExecuteUpdateAsync(e => e.SetProperty(e => e.IsTaken, isTaken));
        }

        public async Task DeleteAsync(Guid key)
        {
            await _context.Doses.Where(e => e.EntityKey == key).ExecuteDeleteAsync();
        }
    }
}
