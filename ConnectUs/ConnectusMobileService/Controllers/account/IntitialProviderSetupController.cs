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
using System.Security.Claims;
using ConnectusMobileService.DataObjects.DTO;
using ConnectusMobileService.Utils.LoginProviders;

namespace ConnectusMobileService.Controllers
{
    /// <summary>
    /// If account is null, this Controller looks wheter an account with the facebookId exists. If none exists a new user will generated.
    /// If account is not null, the account with the given id will be taken and if not yet existing, the facebook id will be added to the account.
    /// </summary>
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class InitialProviderSetupController : ApiController
    {
        public ApiServices Services { get; set; }
        public IServiceTokenHandler handler { get; set; }

        // POST api/FBConnectUser
        public async System.Threading.Tasks.Task<HttpResponseMessage> Get(NetworkType providerType)
        {
            ServiceUser user = this.User as ServiceUser;
            HttpStatusCode httpStatus = HttpStatusCode.OK;
            MobileServiceContext context = new MobileServiceContext();
            Account account = null;
            ProviderCredentials creds = null;
            bool firstLogin = false;

            if (providerType == NetworkType.FACEBOOK)
            {
                creds = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
                if (creds != null)
                {
                    account = context.Accounts.FirstOrDefault(a => a.FacebookId == creds.UserId);
                    if(account == null)
                    {
                        firstLogin = true;
                        account = new Account()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Username =  "-",
                            FacebookId = creds.UserId
                        };
                        context.Accounts.Add(account);
                        UserInfo newUserInfo;
                        context.UserInfos.Add(newUserInfo = new UserInfo()
                        {
                            UserId = account.Id,
                            Id = Guid.NewGuid().ToString()
                        });

                        context.SaveChanges();
                        httpStatus = HttpStatusCode.Created;
                    }
                }
            }

            if (providerType == NetworkType.LINKED_IN)
            {
                creds = (await user.GetIdentitiesAsync()).OfType<LinkedInCredentials>().FirstOrDefault();
                if (creds != null)
                {
                    account = context.Accounts.FirstOrDefault(a => a.LinkedInId == creds.UserId);
                    if (account == null)
                    {
                        firstLogin = true;
                        account = new Account()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Username = "-",
                            LinkedInId = creds.UserId
                        };
                        context.Accounts.Add(account);
                        UserInfo newUserInfo;
                        context.UserInfos.Add(newUserInfo = new UserInfo()
                        {
                            UserId = account.Id,
                            Id = Guid.NewGuid().ToString()
                        });

                        context.SaveChanges();
                        httpStatus = HttpStatusCode.Created;
                    }
                }
            }

            if (account != null)
            {
                var customLoginResult = new ProviderLoginResult()
                {
                    AccountId = account.Id,
                    username = account.Username,
                    FirstLogin = firstLogin
                };
                return this.Request.CreateResponse(httpStatus, customLoginResult);
            }

            return this.Request.CreateResponse(httpStatus);
        }
    }
}
