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
            return await _context.Courses.Where(e => e.AccountId == accountId).OrderByDescending(e => e.IsActive).ThenByDescending(e => e.UpdatedAt).ToListAsync();
        }

        public async Task<long> GetIdAsync(Guid courseKey)
        {
            return await _context.Courses.Where(e => e.EntityKey == courseKey).Select(e => e.Id).SingleAsync();
        }

        public async Task<Course> GetAsync(Guid courseKey)
        {
            return await _context.Courses
                                 .Include(e => e.PrescriptionSchedules)
                                     .ThenInclude(ps => ps.Medicine)
                                 .Include(e => e.PrescriptionSchedules)
                                     .ThenInclude(e => e.Doses)
                                 .SingleAsync(e => e.EntityKey == courseKey);
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
                                 .Where(e => e.Account.EntityKey == accountKey)
                                 .ToListAsync();
        }
    }
}
