namespace EMS.Business.Interface
{
    public interface IMemoryCacheService
    {
        Task<List<string>> GetPermissionsFromRole(string roleIds);
        public Task SetRolePermissionInCache();
    }
}
