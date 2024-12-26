using EmployeeManagementBusiness.Service;
using EmployeeManagementRepository.Repository;
using EMS.Repository;
using EMS.Repository.Interface;
using EMSBusiness.Interface;
using Microsoft.EntityFrameworkCore;

namespace EMS.WebApi.Configurations
{
    public static class DependencyInjection
    {
        public static void ConfigureSqlDependencies(this IServiceCollection services, IConfiguration configuration) //extension method
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection"),
                providerOptions =>
                {
                    providerOptions.CommandTimeout(60);
                }), ServiceLifetime.Scoped);
        }


        public static void ConfigureScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        }
    }
}
