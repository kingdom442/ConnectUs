using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using System.Threading.Tasks;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.Utils.LoginProviders;

namespace ConnectusMobileService.Controllers
{
    public class GetProviderTokenController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/FBToken
        public async Task<string> Get(NetworkType providerType)
        {
            ServiceUser user = this.User as ServiceUser;
            if (user == null)
            {
                throw new InvalidOperationException("Choosen provider is not authenticated with the user at the moment!");
            }
            ProviderCredentials creds = null;
            if (providerType == NetworkType.FACEBOOK)
            {
                creds = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
                if (creds != null)
                    return ((FacebookCredentials)creds).AccessToken;
            }
            else if (providerType == NetworkType.LINKED_IN)
            {
                creds = (await user.GetIdentitiesAsync()).OfType<LinkedInCredentials>().FirstOrDefault();
                if (creds != null)
                    return ((LinkedInCredentials)creds).AccessToken;
            }
           

            throw new InvalidOperationException("Choosen provider is not authenticated with the user at the moment!");
        }

    }
}
