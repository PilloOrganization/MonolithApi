using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly SqlServerAppDbContext _context;

        public MedicineRepository(SqlServerAppDbContext context)
        {
            _context = context;
        }

        public async Task<Medicine> GetAsync(Guid key)
        {
            return await _context.Medicines.SingleAsync(m => m.EntityKey == key);
        }
        public async Task<Medicine?> GetAsync(string name, long userId)
        {
            return await _context.Medicines.SingleOrDefaultAsync(m => m.Name == name && m.UserId == userId);
        }

        public void Create(Medicine medicine)
        {
            _context.Add(medicine);
        }

        public async Task DeleteAsync(Guid key)
        {
            await _context.Medicines.Where(m => m.EntityKey == key).ExecuteDeleteAsync();
        }
    }
}
