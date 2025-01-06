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
        public const string UserModuleAccess = ModuleNameConstant.UserModule + ".ModuleAccess";
        public const string UserModuleCreateUser = ModuleNameConstant.UserModule + ".CreateUsers";
        //public const string UserModuleAssignUserToOrganization = ModuleNameConstant.UserModule + ".ChangeOrganizationsToUser";
        //public const string UserModuleAssignUserToRole = ModuleNameConstant.UserModule + ".AssignUserToRoles";
        //public const string UserModuleUserRoleManagement = ModuleNameConstant.UserModule + ".ManageRoles";
        //public const string UserModuleAddUserRoles = ModuleNameConstant.UserModule + ".AddRoles";
        //public const string UserModuleAssignPermissionToRole = ModuleNameConstant.UserModule + ".ChangePermissionsToRoles";
        //public const string UserModuleResetPassword = ModuleNameConstant.UserModule + ".ResetPassword";
        #endregion

    }

    public static class ModuleNameConstant
    {
        public const string UserModule = "USR";
        //public const string EducationInEmergencyModule = "EIE";
        //public const string SettingsModule = "SETT";
        //public const string StudentsModule = "STUD";
        //public const string StaffModule = "STAFF";
        //public const string ReportModule = "REP";
        //public const string SMCModule = "SMC";
        //public const string InfInventoryModule = "IINV";
        //public const string RealModule = "REAL";
        //public const string ResultModule = "RES";
        //public const string LocalLevelModule = "LCL";
        //public const string DisabilityModule = "DISB";
        //public const string OthersModule = "OTR";
    }
}
