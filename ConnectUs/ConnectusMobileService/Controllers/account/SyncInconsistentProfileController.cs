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
using ConnectusMobileService.Utils.LoginProviders;

namespace connectusmobileservice.controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class SyncInconsistentProfileController : ApiController
    {
        public ApiServices Services { get; set; }

        // Post api/SyncInconsistentProfile
        public async Task<HttpResponseMessage> Post(SyncInconsistentProfileRequest requestParams)
        {
            HttpStatusCode httpStatus = HttpStatusCode.OK;
            MobileServiceContext context = new MobileServiceContext();

            ServiceUser user = this.User as ServiceUser;
            //Todo first userInfo is Empty
            if (user == null)
            {
                throw new InvalidOperationException("This can only be called by authenticated clients");
            }

            UserInfo userInfo = context.UserInfos.Include("UserInfoDetails").FirstOrDefault(ui => ui.UserId == requestParams.accountid);
            if (requestParams.provider == 0 || requestParams.provider == 1)
            {
                FacebookCredentials creds = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
                if (creds != null)
                {
                    ETLProfileDataProvider dataProvider = new FBProfileDataProvider(creds.AccessToken);
                    ProfileData fbProfileData = await dataProvider.GetUserInfo();

                    var detail = (userInfo != null) ? userInfo.UserInfoDetails.FirstOrDefault(x => x.NetworkId == (int)NetworkType.FACEBOOK): new UserInfoDetail();
                    if(detail == null)
                        detail = new UserInfoDetail() { Id = Guid.NewGuid().ToString() };
                    detail.JsonInfo = fbProfileData.AsJsonString();
                    detail.SetNetork(context.Networks.First(n => n.Id == (int)NetworkType.FACEBOOK));

                    if(userInfo == null)
                    {
                        userInfo = new UserInfo();
                        userInfo.Id = Guid.NewGuid().ToString();
                    }
                    userInfo.SetProfileInfo(fbProfileData);
                    if(userInfo.UserInfoDetails != null && !userInfo.UserInfoDetails.Any(x => x.NetworkId == (int)NetworkType.FACEBOOK))
                        userInfo.UserInfoDetails.Add(detail);

                    context.SaveChanges();

                    httpStatus = HttpStatusCode.Created;
                }
            }
            if(requestParams.provider == 0 || requestParams.provider == 2)
            {
                LinkedInCredentials creds = (await user.GetIdentitiesAsync()).OfType<LinkedInCredentials>().FirstOrDefault();
                if (creds != null)
                {
                    ETLProfileDataProvider dataProvider = new LinkedInProfileDataProvider(creds.AccessToken);
                    ProfileData linkedInProfileData = await dataProvider.GetUserInfo();

                    var detail = (userInfo != null) ? userInfo.UserInfoDetails.FirstOrDefault(x => x.NetworkId == (int)NetworkType.LINKED_IN): new UserInfoDetail();
                    if (detail == null)
                        detail = new UserInfoDetail() { Id = Guid.NewGuid().ToString() };
                    detail.JsonInfo = linkedInProfileData.AsJsonString();
                    detail.SetNetork(context.Networks.First(n => n.Id == (int)NetworkType.LINKED_IN));

                    if (userInfo == null)
                    {
                        userInfo = new UserInfo();
                        userInfo.Id = Guid.NewGuid().ToString();
                    }
                    userInfo.SetProfileInfo(linkedInProfileData);
                    if (userInfo.UserInfoDetails != null && !userInfo.UserInfoDetails.Any(x => x.NetworkId == (int)NetworkType.LINKED_IN))
                        userInfo.UserInfoDetails.Add(detail);

                    context.SaveChanges();

                    httpStatus = HttpStatusCode.Created;
                }
            }

            return this.Request.CreateResponse(httpStatus, userInfo);
        }


    }
}
