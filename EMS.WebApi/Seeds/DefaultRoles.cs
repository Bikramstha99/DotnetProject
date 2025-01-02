using EMS.Repository.IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace CEHRD.IEMIS.WebAPI.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Super Admin" });
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
