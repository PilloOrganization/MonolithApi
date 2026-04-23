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

        public async Task<List<PrescriptionSchedule>> GetByCourseIdAsync(long courseId)
        {
            return await _context.PrescriptionSchedules
                                 .Where(e => e.CourseId == courseId)
                                 .Include(e => e.Doses)
                                 .Include(e => e.Medicine).ToListAsync();
        }

        public async Task<List<PrescriptionSchedule>> GetByCourseKeyAsync(Guid courseKey)
        {
            return await _context.PrescriptionSchedules
                                 .Where(e => e.Course.EntityKey == courseKey)
                                 .Include(e => e.Medicine)
                                 .Include(e => e.Doses).ToListAsync();
        }

        public async Task<PrescriptionSchedule> GetAsync(Guid key)
        {
            return await _context.PrescriptionSchedules
                                 .Include(e => e.Medicine)
                                 .Include(e => e.Doses)
                                 .SingleAsync(e => e.EntityKey == key);
        }

        public void Create(PrescriptionSchedule prescriptionSchedule)
        {
            _context.Add(prescriptionSchedule);
        }

        public void Delete(PrescriptionSchedule prescriptionSchedule)
        {
            prescriptionSchedule.Delete();
        }
    }
}
