using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.Constants
{
    public static class PermissionConstant
    {
        #region USER MODULE PERMISSIONS
        public const string UserModuleAccess ="USR.ModuleAccess";
        public const string UserModuleCreateUser = "USR.CreateUsers";
        public const string UserModuleCreateEmployee = "USR.CreateEmployee";
        public const string UserModuleUpdateEmployee = "USR.UpdateEmployee";
        public const string UserModuleDeleteEmployee = "USR.DeleteEmployee";
        public const string UserModuleShowEmployee = "USR.ShowEmployee";

        #endregion

    }

    public static class ModuleNameConstant
    {
        public const string UserModule = "USR";
    }
}
