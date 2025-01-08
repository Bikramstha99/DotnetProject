using EMS.Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.Interface
{
    public interface IUserRepository
    {
        Task<bool> CheckIfUserNameExists(string userName);
        Task<UserApiModel> CreateUser(UserApiModel model);
        Task<IEnumerable<RolesApiModel>> GetAllRoles();
        Task<IEnumerable<RolePermissionMappingApiModel>> GetAllPermissionsByRoleId(string roleId);
        Task<IEnumerable<UserRoleMappingApiModel>> GetUserRoleMapping(string userId);
        Task<bool> AddUserRoleMapping(List<UserRoleMappingApiModel> model);

    }
}
