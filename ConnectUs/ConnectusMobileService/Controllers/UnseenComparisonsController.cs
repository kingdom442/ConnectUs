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
    public class UnseenComparisonsController : ApiController
    {
        public ApiServices Services { get; set; }

        // POST api/UnseenComparisons
        public HttpResponseMessage Post(UnseenComparisonsRequestDTO requestData)
        {
            Services.Log.Info("Entered UnseenComparisons!");
            MobileServiceContext context = new MobileServiceContext();
            List<UserComparisonResultDTO> result = new List<UserComparisonResultDTO>();
            List<UserComparison> comps = context.UserComparisons.Include("User").Include("CompUser")
                    .Where(x => (x.UserId == requestData.UserId || x.CompUserId == requestData.UserId)).OrderByDescending(x => x.CreatedAt).ToList();
            foreach (UserComparison cmp in comps)
            {
                bool currentUserIsInitiator = true; //Initiator of past comparison
                Account otherUser;
                if (cmp.CompUserId == requestData.UserId) {
                    currentUserIsInitiator = false;
                    otherUser = cmp.User;
                }
                else
                {
                    otherUser = cmp.CompUser;
                }
                
                if (!result.Any(x => (x.UserId == otherUser.Id)))
                { 
                    result.Add(new UserComparisonResultDTO()
                    {
                        UserId = otherUser.Id,
                        UserName = otherUser.Username,
                        EqualData = cmp.EqualJson,
                        Date = cmp.CreatedAt,
                        CurrentUserWasInitiator = currentUserIsInitiator,
                        ProfilePic = context.UserInfos.FirstOrDefault(x => x.UserId == otherUser.Id).ProfilePicUrl
                    });
                }
            }
            
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
