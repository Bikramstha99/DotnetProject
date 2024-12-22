using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Entities.Dtos.Account
{
    public class LogoutRequest
    {
        //UserId
        public string UniqueName { get; set; }
        //Cookie Name
        public string Identifier { get; set; }
    }
}
