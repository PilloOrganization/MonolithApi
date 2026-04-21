using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class PrescriptionScheduleRepository : IPrescriptionScheduleRepository
    {
        private readonly SqlServerAppDbContext _context;

        public PrescriptionScheduleRepository(SqlServerAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PrescriptionSchedule>> GetByCourseIdAsync(long courseId)
        {
            return await _context.PrescriptionSchedules.Where(p => p.CourseId == courseId).Include(p => p.Doses).Include(p => p.Medicine).ToListAsync();
        }

        public async Task<IEnumerable<PrescriptionSchedule>> GetByCourseKeyAsync(Guid courseKey)
        {
            Course course = await _context.Courses.Where(c => c.EntityKey == courseKey).Include(c => c.PrescriptionSchedules).SingleAsync();
            return _context.PrescriptionSchedules;
        }

        public void Create(PrescriptionSchedule prescriptionSchedule)
        {
            _context.Add(prescriptionSchedule);
        }

        public async Task DeleteAsync(Guid key)
        {
            await _context.PrescriptionSchedules.Where(p => p.EntityKey == key).ExecuteDeleteAsync();
        }
    }
}
