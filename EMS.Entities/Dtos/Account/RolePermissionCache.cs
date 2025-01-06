using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Entities.Dtos.Account
{
    public class RolePermissionCache
    {
        public string RoleId { get; set; }
        public List<string> Permissions { get; set; }
    }
}
