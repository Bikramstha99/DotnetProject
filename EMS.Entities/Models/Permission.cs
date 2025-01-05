using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Entities.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public int ModuleId { get; set; }
        public int? ParentPermissionId { get; set; }
    }
}
