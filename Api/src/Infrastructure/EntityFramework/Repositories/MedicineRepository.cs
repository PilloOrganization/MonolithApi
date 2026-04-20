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
