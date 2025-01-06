using EMS.Entities.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.Interface
{
    public interface IApplicationUserRepository
    {
        Task<List<RolePermissionCache>> GetRoleWisePermissionList();
    }
}
