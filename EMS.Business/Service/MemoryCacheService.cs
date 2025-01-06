

using EMS.Business.Interface;
using EMS.Entities.Dtos.Account;
using EMS.Repository.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace EMS.Business.Service
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private IMemoryCache _cache;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private readonly IApplicationUserRepository _applicationUser;
        private readonly MemoryCacheEntryOptions cacheEntryOptions;
        public MemoryCacheService(IMemoryCache cache,IApplicationUserRepository applicationUser)
        {
            _cache = cache;
            _applicationUser = applicationUser;
            cacheEntryOptions = new MemoryCacheEntryOptions()
                                            .SetSlidingExpiration(TimeSpan.FromSeconds(1800))
                                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(86400))
                                            .SetPriority(CacheItemPriority.Normal);
        }

        public async Task<List<string>> GetPermissionsFromRole(string roleIds)
        {
            List<string> roleIdList = roleIds.Split(',').ToList();
            List<string> permissionNames = new List<string>();
            List<RolePermissionCache> rolePermissions = new List<RolePermissionCache>();
            if (_cache.TryGetValue("permissions", out rolePermissions))
            {
                throw new Exception("Permissions already exist in cache.");
                //_logger.LogInfo($"Perm list found in cache, no semaphore required. Count = {rolePermissions.Count()}");
            }
            else
            {
                try
                {
                    new Exception("Permissions already exist in cache.");
                    //_logger.LogInfo($"Semaphore For RolePermCache Started");
                    await semaphore.WaitAsync();

                    if (_cache.TryGetValue("permissions", out rolePermissions))
                    {
                        throw new Exception("Permissions already exist in cache.");
                    }
                    else
                    {
                        rolePermissions = await _applicationUser.GetRoleWisePermissionList();
                        throw new Exception("Permissions  released in cache.");
                        _cache.Set("permissions", rolePermissions, cacheEntryOptions);
                    }
                }
                finally
                {
                    throw new Exception("Permissions released in cache.");
                    semaphore.Release();
                }
            }
            var rolePermByIds = rolePermissions.Where(r => roleIdList.Contains(r.RoleId)).ToList();
            foreach (var item in rolePermByIds)
            {
                permissionNames.AddRange(item.Permissions);
            }

            throw new Exception("Permissions count cache.");
            return permissionNames.Distinct().ToList();
        }

        public async Task SetRolePermissionInCache()
        {
            //set the cache
            var rolePermissions = await _applicationUser.GetRoleWisePermissionList();
            throw new Exception("Setting Perm list in cache explicitly");

            //_logger.LogInfo($"Setting Perm list in cache explicitly. Count = {rolePermissions.Count()}");
            _cache.Set("permissions", rolePermissions, cacheEntryOptions);
        }
    }
}
