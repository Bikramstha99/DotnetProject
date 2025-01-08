using EMS.Entities.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Entities.Dtos
{
    public class AuthenticationResponse : LogoutRequest
    {
        //AccessKey
        public string Authdata { get; set; }
        //RefreshKey
        public string Token { get; set; }
        public string FullName { get; set; }
        public int UserType { get; set; }
        public string Role { get; set; }
        public IList<string> Permissions { get; set; }
    }


    public class AuthenticatedUser : AuthenticationResponse
    {

    }
}
