using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.Constant
{
    public enum UserTypeEnum
    {
        [Description("Super Admin")] SuperAdmin = 1,
        [Description("Internal User")] InternalUser = 2,
        [Description("Client User")] ClientUser = 3

    }
}
