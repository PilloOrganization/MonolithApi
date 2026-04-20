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

        public async Task<IEnumerable<Course>> GetCoursesByAccountIdAsync(long accountId)
        {
            return await _context.Courses.Where(c => c.AccountId == accountId).ToListAsync();
        }

        public void Create(Course course)
        {
            _context.Add(course);
        }

        public void Delete(Course course)
        {
            _context.Remove(course);
        }
    }
}
