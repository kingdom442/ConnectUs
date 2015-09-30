using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using ConnectusMobileService.Models;
using ConnectusMobileService.DataObjects.DTO;
using System.Collections;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.Utils;
using Newtonsoft.Json;
using ConnectusMobileService.DataObjects.Profile;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class CompareUsersController : ApiController
    {
        public ApiServices Services { get; set; }

        // POST api/CompareUsers
        public HttpResponseMessage Post(UserComparisonRequestDTO userComparisonRequest)
        {
            //TODO: Check If users were already compared (is comparison up do date?)
            Services.Log.Info("Entered CompareUsers!");
            MobileServiceContext context = new MobileServiceContext();
            if (userComparisonRequest.CompareObjects == null)
                userComparisonRequest.CompareObjects = ProfileData.GetAllProfileDataObjects();

            IEnumerable<UserInfo> userInfos = context.UserInfos.Include("UserInfoDetail").Where(uiDet => uiDet.UserId == userComparisonRequest.UserId);
            IEnumerable <UserInfo> compUserInfos = context.UserInfos.Include("UserInfoDetail").Where(uiDet => uiDet.UserId == userComparisonRequest.CompUserId);

            UserInfo mergedUserInfo = JsonUtil.MergeUserInfoDetails(userInfos);
            UserInfo mergedCompUserInfo = JsonUtil.MergeUserInfoDetails(compUserInfos);

            UserComparison userComp = ProfileComparisonUtil.CompareUserProfiles(JsonConvert.DeserializeObject<ProfileData>(mergedUserInfo.UserInfoDetail.JsonInfo), 
                                                                                   JsonConvert.DeserializeObject<ProfileData>(mergedCompUserInfo.UserInfoDetail.JsonInfo), userComparisonRequest.CompareObjects);
            userComp.Id = Guid.NewGuid().ToString();
            userComp.UserId = userComparisonRequest.UserId;
            userComp.CompUserId = userComparisonRequest.CompUserId;
            context.UserComparisons.Add(userComp);

            context.SaveChanges();
            Services.Log.Info("CompareUsers finished");
            return this.Request.CreateResponse(HttpStatusCode.OK, userComp);
        }
    }
}
