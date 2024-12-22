using EMS.Entities.Dtos;
using EMS.Repository;
using EMS.Repository.IdentityModel;
using EMS.Repository.Interface;
using Microsoft.AspNet.Identity;

namespace EmployeeManagementRepository.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataContext _dbContext;
        public AuthenticationRepository(DataContext dbContext, UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (user.AccessFailedCount >= 20)
                    {
                        throw new Exception("This user is locked out");
                    }
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        if (user.AccessFailedCount != 0)
                        {
                            user.AccessFailedCount = 0;
                            _dbContext.Users.Update(user);
                            _dbContext.Entry(user).Property(p => p.AccessFailedCount).IsModified = true;
                            await _dbContext.SaveChangesAsync();
                        }

                        //return await GetUserWithTokenDetailsFromApplicationUser(user);
                    }
                    else
                    {
                        user.AccessFailedCount += 1;
                        _dbContext.Users.Update(user);
                        _dbContext.Entry(user).Property(p => p.AccessFailedCount).IsModified = true;

                        await _dbContext.SaveChangesAsync();
                        int attemptLeft = 20 - user.AccessFailedCount;
                        throw new Exception($"Invalid username and password found. Your account will be locked after {attemptLeft} more attempts.");
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error in User reposiroty authentication: {ex.Message}");
                throw;
            }
        }
    }
}
