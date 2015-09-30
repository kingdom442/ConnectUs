using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using System.Threading.Tasks;
using Facebook;
using ConnectusMobileService.Service;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.Models;
using ConnectusMobileService.DataObjects.DTO;
using ConnectusMobileService.DataObjects.Profile;
using System.Data.Entity.Migrations;


namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class FBSyncProfileController : ApiController
    {
        public ApiServices Services { get; set; }

        // Post api/FBGetProfile
        public async Task<HttpResponseMessage> Post(AccountId accountid)
        {
            HttpStatusCode httpStatus = HttpStatusCode.OK;
            MobileServiceContext context = new MobileServiceContext();

            ServiceUser user = this.User as ServiceUser;
            if (user == null)
            {
                throw new InvalidOperationException("This can only be called by authenticated clients");
            }
            FacebookCredentials creds = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
            if (creds != null)
            {
                var fbDataProvider =  new FBProfileDataProvider(creds.AccessToken);
                Network facebook = context.Networks.First(n => n.Id == (Int16)NetworkType.FACEBOOK);
                ProfileData fbProfileData = await fbDataProvider.GetUserInfo();
                UserInfo userInfo;
                if (context.UserInfos.Any(ui => ui.UserId == accountid.accountid && ui.NetworkId == facebook.Id))
                {
                    userInfo = context.UserInfos.First(ui => ui.UserId == accountid.accountid && ui.NetworkId == facebook.Id);
                    userInfo.Description = fbProfileData.Description;
                    userInfo.ProfilePicUrl = fbProfileData.ProfilePicUrl;
                    // TODO: Update other propoerties
                }
                else
                {
                    userInfo = new UserInfo(fbProfileData) { UserId = accountid.accountid };
                    userInfo.SetNetork(facebook);
                    userInfo.Id = Guid.NewGuid().ToString();
                    context.UserInfos.Add(userInfo);
         
                    httpStatus = HttpStatusCode.Created;
                }

                UserInfoDetail userInfoDetail = new UserInfoDetail(userInfo.Id, fbProfileData.AsJsonString());
                context.UserInfoDetails.AddOrUpdate(userInfoDetail);
                context.SaveChanges();
                return this.Request.CreateResponse(httpStatus, userInfo);
            }

            throw new Exception("Facebook identity not found");
        }


    }
}
