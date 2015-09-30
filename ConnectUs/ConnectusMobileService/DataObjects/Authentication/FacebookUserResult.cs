using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.Authentication
{
    public class FacebookConnectUserRequest
    {
        public string AccountId { get; set; }
        public string UserId { get; set; }
        public string authenticationtoken { get; set; }
    }
}