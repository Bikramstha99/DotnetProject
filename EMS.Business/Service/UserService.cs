using EMS.Business.Interface;
using EMS.Entities.Dtos;
using EMS.Entities.Dtos.User;
using EMS.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckIfUserNameExists(string userName)
        {
            return await _userRepository.CheckIfUserNameExists(userName);
        }
        public async Task<UserApiModel> CreateUser(UserApiModel model)
        {
            return await _userRepository.CreateUser(model);
        }
        public async Task<IEnumerable<RolesApiModel>> GetAllRoles()
        {
            return await _userRepository.GetAllRoles();
        }
        public async Task<IEnumerable<RolePermissionMappingApiModel>> GetAllPermissionsByRoleId(string roleId)
        {
            return await _userRepository.GetAllPermissionsByRoleId(roleId);
        }
        public async Task<IEnumerable<UserRoleMappingApiModel>> GetUserRoleMapping(string userId)
        {
            return await _userRepository.GetUserRoleMapping(userId);
        }
        public async Task<bool> AddUserRoleMapping(List<UserRoleMappingApiModel> model)
        {
            return await _userRepository.AddUserRoleMapping(model);
        }
    }
}
