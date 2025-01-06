using CEHRD.IEMIS.Entities.Dtos.Account;
using CEHRD.IEMIS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CEHRD.IEMIS.Repository.Repository
{
    public class ApplicationUserRepository : EFCoreRepository, IApplicationUserRepository
    {
        public ApplicationUserRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<RolePermissionCache>> GetRoleWisePermissionList()
        {
            return await (from perm in _dbContext.Permissions.Where(p => p.IsActive).AsNoTracking()
                          join map in _dbContext.UserRolePermissionMappings.Where(rp => !rp.IsDeleted).AsNoTracking()
                          on perm.Id equals map.PermissionId
                          select new
                          {
                              perm.PermissionName,
                              map.RoleId
                          }).GroupBy(g => g.RoleId).Select(s => new RolePermissionCache
                          {
                              RoleId= s.Key,
                              Permissions = s.Select(p => p.PermissionName).ToList()
                          }).ToListAsync();
        }
    }
}
