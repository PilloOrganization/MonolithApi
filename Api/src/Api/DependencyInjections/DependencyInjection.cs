using Api.AutomapperProfiles;
using Api.Services;
using Domain.Services.Interfaces;

namespace Api.DependencyInjections
{
    public static class DependencyInjection
    {
        public static void AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutomapper(services);
            services.AddAuthentication(configuration);
            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();
        }

        private static void AddAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CourseProfile>();
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
