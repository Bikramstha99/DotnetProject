using EMS.Common.Constant;
using EMS.Repository.IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace CEHRD.IEMIS.WebAPI.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                //Seed Default User
                var defaultUser = new ApplicationUser
                {
                    UserName = "bikramshrestha",
                    Email = "superadmin@gmail.com",
                    UserType = UserTypeEnum.SuperAdmin,
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = "super",
                    LastName = "admin",
                    Gender = "male",
                };


                if (await userManager.FindByNameAsync(defaultUser.UserName) == null)
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);
                    if (user == null)
                    {
                        var res = await userManager.CreateAsync(defaultUser, "p@ssw0rdEEEE");
                        if (res.Succeeded)
                        {
                            await userManager.AddToRoleAsync(defaultUser, "Super Admin");
                        }
                    }
                    //if needed claims then it can be added here as well
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
