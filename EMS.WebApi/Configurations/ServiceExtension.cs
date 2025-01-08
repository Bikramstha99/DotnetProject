using EMS.Common.Logger;
using EMS.Repository.IdentityModel;
using EMS.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EMS.WebApi.Configurations
{
    public static class ServiceExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"])),
                    ClockSkew = TimeSpan.FromMinutes(5)  // Allow some clock skew
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // Log the error
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
            });
        }

        public static void ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

            services.AddIdentityCore<ApplicationUser>(options => { });//for customizing identity

        }
    }
}
