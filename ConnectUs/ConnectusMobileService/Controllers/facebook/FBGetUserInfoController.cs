using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ConnectusMobileService.Controllers
{
    public class FBUserInfoController : ApiController
    {
        public ApiServices Services { get; set; }

        [AuthorizeLevel(AuthorizationLevel.User)]
        public async Task<JObject> Get()
        {
            ServiceUser user = this.User as ServiceUser;
            if (user == null)
            {
                throw new InvalidOperationException("This can only be called by authenticated clients");
            }
            FacebookCredentials creds = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
            var result = new JObject();
            if (creds != null)
            {
                string accessToken = creds.AccessToken;
                result.Add("facebook", await GetProviderInfo("https://graph.facebook.com/me?access_token=" + accessToken));
            }

            return result;
        }

        private async Task<JToken> GetProviderInfo(string url)
        {
            var c = new HttpClient();
            var resp = await c.GetAsync(url);
            resp.EnsureSuccessStatusCode();
            return JToken.Parse(await resp.Content.ReadAsStringAsync());
        }
    }
}
