using Application.UnitOfWorks.Interfaces;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.UnitOfWorks;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DependencyInjections
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SqlServerAppDbContext>(options => options.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information));
            AddRepositoriesAndUnitOfWork(services);
            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }

        private static void AddRepositoriesAndUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IDoseRepository, DoseRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IPrescriptionScheduleRepository, PrescriptionScheduleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
