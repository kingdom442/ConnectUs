using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.Authentication
{
    public class LoginRequest
    {
        public String username { get; set; }
        public String password { get; set; }
    }
}