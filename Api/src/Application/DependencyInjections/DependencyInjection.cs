using Application.AutomapperProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjections
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });
            AddAutomapper(services);
        }

        private static void AddAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AccountProfile>();
            }, typeof(DependencyInjection));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CourseProfile>();
            }, typeof(DependencyInjection));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DoseProfile>();
            }, typeof(DependencyInjection));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MedicineProfile>();
            }, typeof(DependencyInjection));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<PrescriptionScheduleProfile>();
            }, typeof(DependencyInjection));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            }, typeof(DependencyInjection));
        }
    }
}
