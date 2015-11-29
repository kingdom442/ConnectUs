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
using ConnectusMobileService.Service.Data;

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

            UserInfo userInfo = context.UserInfos.Include("UserInfoDetails").FirstOrDefault(uiDet => uiDet.UserId == userComparisonRequest.UserId);
            UserInfo compUserInfo = context.UserInfos.Include("UserInfoDetails").FirstOrDefault(uiDet => uiDet.UserId == userComparisonRequest.CompUserId);

            UserInfoDetail mergedUserInfo = JsonUtil.MergeUserInfoDetails(userInfo.UserInfoDetails);
            UserInfoDetail mergedCompUserInfo = JsonUtil.MergeUserInfoDetails(compUserInfo.UserInfoDetails);

            UserComparison userComp = ProfileComparisonUtil.CompareUserProfiles(JsonConvert.DeserializeObject<ProfileData>(mergedUserInfo.JsonInfo), 
                                                                                   JsonConvert.DeserializeObject<ProfileData>(mergedCompUserInfo.JsonInfo), userComparisonRequest.CompareObjects);
            userComp.Id = Guid.NewGuid().ToString();
            userComp.UserId = userComparisonRequest.UserId;
            userComp.CompUserId = userComparisonRequest.CompUserId;

            userComp.EqualConnections = EqualConnectionsService.GetEqualConnections(userComparisonRequest.UserId, userComparisonRequest.CompUserId);
            context.UserComparisons.Add(userComp);

            context.SaveChanges();
            Services.Log.Info("CompareUsers finished");
            return this.Request.CreateResponse(HttpStatusCode.OK, userComp);
        }
    }
}
