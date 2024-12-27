using EmployeeManagementBusiness.Service;
using EmployeeManagementRepository.Repository;
using EMS.Business.Interface;
using EMS.Business.Service;
using EMS.Repository;
using EMS.Repository.Interface;
using EMS.Repository.Repository;
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
            services.AddScoped<IEmployeeService, EmployeeService>();


            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        }
    }
}
