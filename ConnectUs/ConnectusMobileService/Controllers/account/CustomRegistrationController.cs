using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using System.Text.RegularExpressions;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.Models;
using ConnectusMobileService.Utils;
using ConnectusMobileService.DataObjects.Authentication;

namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.Anonymous)]
    public class CustomRegistrationController : ApiController
    {
        public ApiServices Services { get; set; }

        // POST api/CustomRegistration
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            if (!Regex.IsMatch(registrationRequest.username, "^[a-zA-Z0-9]{4,}$"))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid username (at least 4 chars, alphanumeric only)");
            }
            else if (registrationRequest.password.Length < 8)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid password (at least 8 chars required)");
            }

            MobileServiceContext context = new MobileServiceContext();
            Account account;
            try {
                 account = context.Accounts.Where(a => a.Username == registrationRequest.username).SingleOrDefault();
            } catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            if (account != null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "That username already exists.");
            }
            else
            {
                byte[] salt = LoginProviderUtil.generateSalt();
                Account newAccount = new Account
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = registrationRequest.username,
                    Salt = salt,
                    SaltedAndHashedPassword = LoginProviderUtil.hash(registrationRequest.password, salt)
                };
                context.Accounts.Add(newAccount);
                context.UserInfos.Add(new UserInfo() { Description = "About me", UserId = account.Id });
                context.SaveChanges();

                return this.Request.CreateResponse(HttpStatusCode.Created);
            }
        }
    }
}
