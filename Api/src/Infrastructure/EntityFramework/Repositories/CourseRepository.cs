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

        public async Task<IEnumerable<Course>> GetByAccountIdAsync(long accountId)
        {
            return await _context.Courses.Where(c => c.AccountId == accountId).ToListAsync();
        }

        public async Task<long> GetIdAsync(Guid courseKey)
        {
            return await _context.Courses.Where(c => c.EntityKey == courseKey).Select(e => e.Id).SingleAsync();
        }

        public async Task<Course> GetAsync(Guid courseKey)
        {
            return await _context.Courses
                                 .Include(c => c.PrescriptionSchedules)
                                     .ThenInclude(ps => ps.Medicine)
                                 .Include(c => c.PrescriptionSchedules)
                                     .ThenInclude(ps => ps.Doses)
                                 .SingleAsync(c => c.EntityKey == courseKey);
        }

        public void Create(Course course)
        {
            _context.Add(course);
        }

        public void Delete(Course course)
        {
            _context.Remove(course);
        }

        public async Task<IEnumerable<Course>> GetByAccountKeyAsync(Guid accountKey)
        {
            return await _context.Courses
                                 .Where(c => c.Account.EntityKey == accountKey)
                                 .ToListAsync();
        }
    }
}
