using EMS.Repository;
using Microsoft.EntityFrameworkCore;

namespace EMS.WebApi.Configurations
{
    public static class DependencyInjection
    {
        public static void ConfigureSqlDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection"),
                providerOptions =>
                {
                    providerOptions.CommandTimeout(60);
                }), ServiceLifetime.Scoped);
        }


        public static void ConfigureScopedServices(this IServiceCollection services)
        {
        }
    }
}
