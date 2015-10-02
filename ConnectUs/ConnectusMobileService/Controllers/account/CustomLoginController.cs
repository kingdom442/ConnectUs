using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using System.Security.Claims;
using ConnectusMobileService.DataObjects.Authentication;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.Models;
using ConnectusMobileService.Utils;

namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.Anonymous)]
    public class CustomLoginController : ApiController
    {
        public ApiServices Services { get; set; }
        public IServiceTokenHandler handler { get; set; }

        // POST api/CustomLogin
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            MobileServiceContext context = new MobileServiceContext();
            Account account = context.Accounts.SingleOrDefault(a => a.Username == loginRequest.username);
            if (account != null)
            {
                byte[] incoming = LoginProviderUtil
                    .hash(loginRequest.password, account.Salt);

                if (LoginProviderUtil.slowEquals(incoming, account.SaltedAndHashedPassword))
                {
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginRequest.username));
                    LoginResult loginResult = new CustomLoginProvider(handler)
                        .CreateLoginResult(claimsIdentity, Services.Settings.MasterKey);
                    var customLoginResult = new CustomLoginResult()
                    {
                        AccountId = account.Id,
                        UserId = loginResult.User.UserId,
                        username = account.Username,
                        MobileServiceAuthenticationToken = loginResult.AuthenticationToken
                    };
                    return this.Request.CreateResponse(HttpStatusCode.OK, customLoginResult);
                }
            }
            return this.Request.CreateResponse(HttpStatusCode.Unauthorized,
                "Invalid username or password");
        }
    }
}
