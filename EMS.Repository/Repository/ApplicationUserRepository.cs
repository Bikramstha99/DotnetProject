using EMS.Entities.Dtos.Account;
using EMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly DataContext _dbContext;

        public ApplicationUserRepository(DataContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<RolePermissionCache>> GetRoleWisePermissionList()
        {
            return await (from perm in _dbContext.Permissions.Where(p => p.IsActive).AsNoTracking()
                          join map in _dbContext.UserRolePermissionMappings.AsNoTracking()
                          on perm.Id equals map.PermissionId
                          select new
                          {
                              perm.PermissionName,
                              map.RoleId
                          }).GroupBy(g => g.RoleId).Select(s => new RolePermissionCache
                          {
                              RoleId = s.Key,
                              Permissions = s.Select(p => p.PermissionName).ToList()
                          }).ToListAsync();
        }
    }
}
