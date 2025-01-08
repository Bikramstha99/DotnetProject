using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Entities.Dtos.User
{
    public class RolePermissionMappingApiModel
    {
        public int PermissionId { get; set; }
        public bool IsSelected { get; set; }
        public string PermissionName { get; set; }
        public string DisplayName { get; set; }
        public int? ParentPermissionId { get; set; }
    }
}
