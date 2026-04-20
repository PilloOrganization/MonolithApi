using Api.AutomapperProfiles;

namespace Api.DependencyInjections
{
    public static class DependencyInjection
    {
        public static void AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutomapper(services);
            services.AddAuthentication(configuration);
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
