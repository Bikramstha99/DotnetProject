using EMS.Entities.Dtos;
using EMS.Entities.Dtos.User;
using EMS.Repository.IdentityModel;
using EMS.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbContext,  UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }
        public async Task<bool> CheckIfUserNameExists(string userName)
        {
            bool exists = await _userManager.FindByNameAsync(userName) != null;
            return exists;
        }
        public async Task<UserApiModel> CreateUser(UserApiModel model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    UserType = model.UserType,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                };

                if (await _userManager.FindByNameAsync(user.UserName) == null)
                {
                    var res = await _userManager.CreateAsync(user, model.Password);
                    if (!res.Succeeded)
                    {
                        throw new Exception("Problem is creating the user.");
                    }
                    else
                    {
                        model.Id = user.Id;
                        return model;
                    }
                }
                else
                {
                    throw new Exception("Problem is creating the user. User already exists.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<RolesApiModel>> GetAllRoles()
        {
            return await _roleManager.Roles.Select(r => new RolesApiModel
            {
                RoleId = r.Id,
                RoleName = r.Name,
            }).ToListAsync();
        }

        public async Task<IEnumerable<RolePermissionMappingApiModel>> GetAllPermissionsByRoleId(string roleId)
        {
            return await (from p in _dbContext.Permissions.AsNoTracking()
                          join rp in _dbContext.UserRolePermissionMappings.Where(rpm => rpm.RoleId == roleId).AsNoTracking()
                          on p.Id equals rp.PermissionId into dt
                          from rp in dt.DefaultIfEmpty()
                          select new RolePermissionMappingApiModel
                          {
                              PermissionId = p.Id,
                              IsSelected = rp == null ? false : true,
                              PermissionName = p.DisplayName,
                              DisplayName = p.DisplayName,
                              ParentPermissionId = p.ParentPermissionId
                          }).ToListAsync();
        }
        public async Task<IEnumerable<UserRoleMappingApiModel>> GetUserRoleMapping(string userId)
        {
            return await (from role in _dbContext.Roles.Where(r => r.Name != "Super Admin").AsNoTracking()
                          join map in _dbContext.UserRoles.Where(u => u.UserId == userId).AsNoTracking() on role.Id equals map.RoleId into tempRole
                          from rm in tempRole.DefaultIfEmpty()
                          select new UserRoleMappingApiModel
                          {
                              RoleId = role.Id,
                              RoleName = role.Name,
                              UserId = userId,
                              IsSelected = (rm == null ? false : true)
                          }).ToListAsync();
        }
        public async Task<bool> AddUserRoleMapping(List<UserRoleMappingApiModel> model)
        {
            List<string> rolesUsed = model.Select(s => s.RoleId).ToList();
            string userId = model[0].UserId;

            var existingRoleMappings = await _dbContext.UserRoles.Where(r => r.UserId == userId
                                        && ((_dbContext.Roles.Where(mr => mr.Id == r.RoleId).Any()))).ToListAsync();
            _dbContext.UserRoles.RemoveRange(existingRoleMappings);
            _dbContext.SaveChanges();

            var user = await _userManager.FindByIdAsync(userId);
            foreach (var item in model.Where(r => r.IsSelected == true).ToList())
            {
                await _userManager.AddToRoleAsync(user, item.RoleName);
            }
            return true;
        }
    }
}
