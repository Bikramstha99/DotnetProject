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
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            //var origin = configuration["AppSettings:Audience"];
            services.AddCors(options =>
            {
                options.AddPolicy(name: "EMSPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Validate the JWT Issuer (iss) claim
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,  
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"])),
                    ClockSkew = TimeSpan.Zero
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
