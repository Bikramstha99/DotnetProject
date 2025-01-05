using EMS.Common.Constant;
using EMS.Entities.Dtos;
using EMS.Repository;
using EMS.Repository.IdentityModel;
using EMS.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementRepository.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataContext _dbContext;
        private readonly IConfiguration _config;

        public AuthenticationRepository(DataContext dbContext, UserManager<ApplicationUser> userManager, IConfiguration config) 
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _config = config;
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
                    if (true)
                    {
                        if (user.AccessFailedCount != 0)
                        {
                            user.AccessFailedCount = 0;
                            _dbContext.Users.Update(user);
                            _dbContext.Entry(user).Property(p => p.AccessFailedCount).IsModified = true;
                            await _dbContext.SaveChangesAsync();
                        }

                        return await GetUserWithTokenDetailsFromApplicationUser(user);
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
        private async Task<AuthenticatedUser> GetUserWithTokenDetailsFromApplicationUser(ApplicationUser user)
        {
            var authenticatedUser = await GetUsersAndTheirRolesAndPermissions(user);
            authenticatedUser.Authdata = GenerateJwtToken(authenticatedUser);
            return authenticatedUser;
        }

        private async Task<AuthenticatedUser> GetUsersAndTheirRolesAndPermissions(ApplicationUser identityUser)
        {
            var isSuperAdmin = (int)identityUser.UserType == (int)UserTypeEnum.SuperAdmin;
            var assignedRoles = await _dbContext.UserRoles.Where(r => r.UserId == identityUser.Id).Select(s => s.RoleId).AsNoTracking().ToListAsync();

            var user = new AuthenticatedUser
            {
                UniqueName = identityUser.Id,
                Role = string.Join(',', assignedRoles),
                FullName = identityUser.FirstName + " " + identityUser.LastName,
                UserType = (int)identityUser.UserType,
            };
            user.Permissions = new List<string>();
            if (((assignedRoles != null) && (assignedRoles.Count() > 0)) || isSuperAdmin)
            {
                if (isSuperAdmin)
                {
                    var permList = await _dbContext.Permissions.Where(p => p.IsActive).AsNoTracking().ToListAsync();
                    user.Permissions = permList.Select(p => p.PermissionName).ToList();
                }
                else
                {
                    user.Permissions = (from rp in await _dbContext.UserRolePermissionMappings.Where(u => assignedRoles.Contains(u.RoleId)).AsNoTracking().ToListAsync()
                                        join p in await _dbContext.Permissions.AsNoTracking().ToListAsync() on rp.PermissionId equals p.Id
                                        select p.PermissionName
                                       ).ToList();
                }
            }
            return user;
        }

        private string GenerateJwtToken(AuthenticatedUser user)
        {
            bool isSuperAdmin = user.UserType == (int)UserTypeEnum.SuperAdmin;

            var authclaims = new[] {
                new Claim(ClaimConstants.UniqueName, user.UniqueName),
                new Claim(ClaimConstants.Role, user.Role),
                new Claim(ClaimConstants.UserType, user.UserType.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Secret").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var expiretime = DateTime.Now.AddHours(1);
            var tokenDescriptor = new JwtSecurityToken(
                claims: authclaims,
                expires: expiretime,
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return jwt;
        }
    }
}
