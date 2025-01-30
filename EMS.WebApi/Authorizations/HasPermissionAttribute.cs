using EMS.Common.Constant;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using EMS.Business.Interface;

namespace EMS.WebApi.Authorizations
{
    public sealed class HasPermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<string> _permissions;

        public HasPermissionAttribute(params string[] permissions)
        {
            _permissions = permissions ?? new string[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roleClaim = context.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimConstants.Role)?.Value;
            var userTypeVal = context.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimConstants.UserType)?.Value;
            bool isSuperAdminUserType = (UserTypeEnum)Convert.ToInt16(userTypeVal) == UserTypeEnum.SuperAdmin;

            if (string.IsNullOrWhiteSpace(roleClaim))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
            else
            {
                var cacheService = context.HttpContext.RequestServices.GetService<IMemoryCacheService>();
                var permissionList = cacheService?.GetPermissionsFromRole(roleClaim).GetAwaiter().GetResult();
                if ((permissionList == null || _permissions.Any() && !_permissions.Intersect(permissionList).Any()) && !isSuperAdminUserType)
                {
                    // not logged in or role not authorized
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
            }

        }
    }
}
