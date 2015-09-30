using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using System.Threading.Tasks;

namespace ConnectusMobileService.Controllers
{
    public class FBTokenController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/FBToken
        public async Task<string> Get()
        {
            ServiceUser user = this.User as ServiceUser;
            if (user == null)
            {
                throw new InvalidOperationException("This can only be called by authenticated clients");
            }
            FacebookCredentials creds = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
            if (creds != null)
            {
                string accessToken = creds.AccessToken;
                return accessToken;
            }

            return "";
        }

    }
}
