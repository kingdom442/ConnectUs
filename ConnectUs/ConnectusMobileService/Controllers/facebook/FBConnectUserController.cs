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

namespace ConnectusMobileService.Controllers
{
    /// <summary>
    /// If account is null, this Controller looks wheter an account with the facebookId exists. If none exists a new user will generated.
    /// If account is not null, the account with the given id will be taken and if not yet existing, the facebook id will be added to the account.
    /// </summary>
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class FBConnectUserController: ApiController
    {
        public ApiServices Services { get; set; }
        public IServiceTokenHandler handler { get; set; }

        // POST api/FBConnectUser
        public HttpResponseMessage Post(FacebookConnectUserRequest fbConnectUserRequest)
        {
            HttpStatusCode httpStatus = HttpStatusCode.OK;
            MobileServiceContext context = new MobileServiceContext();
            Account account;
            if (fbConnectUserRequest.AccountId == null)
            {
                account = context.Accounts.FirstOrDefault(a => a.FacebookId == fbConnectUserRequest.UserId);
                if (account == null)
                {
                    account = new Account()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Username = fbConnectUserRequest.UserId,
                        FacebookId = fbConnectUserRequest.UserId
                    };
                    context.Accounts.Add(account);
                    context.SaveChanges();
                    httpStatus = HttpStatusCode.Created;
                }
            }
            else
            {
                if(context.Accounts.Any(a => a.FacebookId == fbConnectUserRequest.UserId && a.Id != fbConnectUserRequest.AccountId))
                {
                    httpStatus = HttpStatusCode.Forbidden; // A facebook account can only be connected to one user
                    return this.Request.CreateResponse(httpStatus, "The given facebook account is already connected to another user!");
                }
                account = context.Accounts.First(a => a.Id == fbConnectUserRequest.AccountId);
                if (account == null)
                {
                    httpStatus = HttpStatusCode.NotFound;
                    return this.Request.CreateResponse(httpStatus, "No account found for the given parameter!");
                } else if(account.FacebookId == null || account.FacebookId != fbConnectUserRequest.UserId)
                {
                    account.FacebookId = fbConnectUserRequest.UserId; //Set the new FacebookId for the given account
                    context.SaveChanges();
                }
            }
            if (account != null)
            {
                var customLoginResult = new FBCustomLoginResult()
                {
                    AccountId = account.Id,
                    UserId = fbConnectUserRequest.UserId,
                    username = account.Username,
                    MobileServiceAuthenticationToken = fbConnectUserRequest.authenticationtoken,
                    facebookid = account.FacebookId
                };
                return this.Request.CreateResponse(httpStatus, customLoginResult);
            } 
            
            return this.Request.CreateResponse(httpStatus);
        }
    }
}
