using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SqlServerAppDbContext _context;

        public CourseRepository(SqlServerAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetByAccountIdAsync(long accountId)
        {
            return await _context.Courses
                                 .Where(e => e.AccountId == accountId)
                                 .OrderByDescending(e => e.IsActive)
                                    .ThenByDescending(e => e.UpdatedAt)
                                 .ToListAsync();
        }

        public async Task<List<Course>> GetByAccountKeyAsync(Guid accountKey)
        {
            return await _context.Courses
                                 .Where(e => e.Account.EntityKey == accountKey)
                                 .OrderByDescending(e => e.IsActive)
                                    .ThenByDescending(e => e.UpdatedAt)
                                 .ToListAsync();
        }

        public async Task<Course> GetAsync(long id)
        {
            return await _context.Courses
                                 .Include(e => e.PrescriptionSchedules)
                                     .ThenInclude(ps => ps.Medicine)
                                 .Include(e => e.PrescriptionSchedules)
                                     .ThenInclude(e => e.Doses)
                                 .SingleAsync(e => e.Id == id);
        }

        public async Task<Course> GetAsync(Guid key)
        {
            return await _context.Courses
                                 .Include(e => e.PrescriptionSchedules)
                                     .ThenInclude(ps => ps.Medicine)
                                 .Include(e => e.PrescriptionSchedules)
                                     .ThenInclude(e => e.Doses)
                                 .SingleAsync(e => e.EntityKey == key);
        }

        public async Task<long> GetIdAsync(Guid courseKey)
        {
            return await _context.Courses.Where(e => e.EntityKey == courseKey).Select(e => e.Id).SingleAsync();
        }

        public void Create(Course course)
        {
            _context.Add(course);
        }

        public void Delete(Course course)
        {
            course.Delete();
        }
    }
}
