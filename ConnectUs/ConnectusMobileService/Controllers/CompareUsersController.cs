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
            Services.Log.Info("Entered CompareUsers!");
            MobileServiceContext context = new MobileServiceContext();
            UserComparison lastUserComparison = context.UserComparisons.Where(x => (x.UserId == userComparisonRequest.UserId && x.CompUserId == userComparisonRequest.CompUserId) || (x.UserId == userComparisonRequest.CompUserId && x.CompUserId == userComparisonRequest.UserId)).OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            if(lastUserComparison != null)
            {
                if(!context.UserInfos.Any(x => x.UserId == userComparisonRequest.UserId && x.UpdatedAt > lastUserComparison.CreatedAt) &&
                    !context.UserInfos.Any(x => x.UserId == userComparisonRequest.UserId && x.UpdatedAt > lastUserComparison.CreatedAt))
                {
                    //No Profile changes since last update
                    return this.Request.CreateResponse(HttpStatusCode.OK, lastUserComparison);
                }
            }

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
