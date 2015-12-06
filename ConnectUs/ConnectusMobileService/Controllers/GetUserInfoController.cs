using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.DataObjects.DTO;
using ConnectusMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class GetUserInfoController : ApiController
    {
        public ApiServices Services { get; set; }

        // Post api/GetUserInfo
        public FullUserInfoDTO Post(AccountId accountid)
        {
            MobileServiceContext context = new MobileServiceContext();
            FullUserInfoDTO userinfo = new FullUserInfoDTO();
            userinfo.AccountId = accountid.accountid;

            Account a = context.Accounts.FirstOrDefault(x => x.Id == accountid.accountid);
            if (a.FacebookId != null)
            {
                userinfo.FacebookProfileLink = "https://facebook.com/" + a.FacebookId;
                userinfo.FacebookConnected = true;
            }
            else
            {
                userinfo.FacebookConnected = false;
            }
            if (a.LinkedInId != null)
            {
                userinfo.LinkedInProfileLink = "http://www.linkedin.com/profile/view?id=" + a.LinkedInId;
                userinfo.LinkedInConnected = true;
            }
            else
            {
                userinfo.LinkedInConnected = false;
            }

            if (!context.UserInfos.Any(uinfo => uinfo.UserId == accountid.accountid))
            {
                context.UserInfos.Add(new UserInfo() { Id = Guid.NewGuid().ToString(), Bio = "About me", UserId = accountid.accountid });
                context.SaveChanges();

            }
            UserInfo ui = context.UserInfos.Include("Account").First(uinfo => uinfo.UserId == accountid.accountid);
            userinfo.UserInfoId = ui.Id;
            userinfo.Age = ui.Age;
            userinfo.Status = ui.Bio;
            userinfo.About = ui.About;
            userinfo.Gender = ui.Gender;
            userinfo.Username = ui.Account.Username;
            userinfo.FirstName = ui.FirstName;
            userinfo.LastName = ui.LastName;
            userinfo.ProfilePicUrl = ui.ProfilePicUrl;
            userinfo.BusinessInterested = a.BusinessInterest;
            userinfo.PrivateInterested = a.PrivateInterest;
            
            UserContact userContact = context.UserContacts.FirstOrDefault(contact => contact.UserId == accountid.accountid);
            if(userContact != null)
            {
                userinfo.PhoneNr = userContact.PhoneNr;
                userinfo.Email = userContact.Email;
            }
            return userinfo;
        }

    }
}
