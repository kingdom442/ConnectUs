using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.Authentication
{
    public class CustomLoginResult
    {
        public string AccountId { get; set; }
        public string UserId { get; set; }
        public string username { get; set; }
        public string MobileServiceAuthenticationToken { get; set; }
    }

    public class ProviderLoginResult: CustomLoginResult
    {
        public bool FirstLogin { get; set; }
    }
}