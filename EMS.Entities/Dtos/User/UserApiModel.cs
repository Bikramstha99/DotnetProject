using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Common.Constant;

namespace EMS.Entities.Dtos.User
{
    public class UserApiModel
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Gender { get; set; }
        public UserTypeEnum UserType { get; set; }
        [NotMapped]
        public string Password { get; set; }
    }
}
