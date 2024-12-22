using EMS.Common.Constant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSBusiness
{
    public class UserResolver
    {
        public readonly string UserType;
        public readonly string UserId;
        public readonly int District;
        public readonly bool IsInternalUser;
        public readonly bool IsClientUser;
        public readonly bool IsSuperAdmin;
        public readonly IHttpContextAccessor httpCtx;
        public UserResolver(IHttpContextAccessor _httpCtx)
        {
            httpCtx = _httpCtx;
            var userTypeClaim = httpCtx?.HttpContext?.User.Claims.FirstOrDefault(e => e.Type == ClaimConstants.UserType);
            var userId = httpCtx?.HttpContext?.User.Claims.FirstOrDefault(e => e.Type == ClaimConstants.UniqueName);
            UserType = userTypeClaim == null ? "" : userTypeClaim.Value;
            UserId = userId == null ? string.Empty : userId.Value;
            IsSuperAdmin = Convert.ToInt16(userTypeClaim == null ? "0" : userTypeClaim.Value) == (int)UserTypeEnum.SuperAdmin;
            IsInternalUser = Convert.ToInt16(userTypeClaim == null ? "0" : userTypeClaim.Value) == (int)UserTypeEnum.InternalUser;
            IsClientUser = Convert.ToInt16(userTypeClaim == null ? "0" : userTypeClaim.Value) == (int)UserTypeEnum.ClientUser;
        }
    }
    
}
