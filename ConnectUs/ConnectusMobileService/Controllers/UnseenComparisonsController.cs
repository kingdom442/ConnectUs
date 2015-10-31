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
            foreach(UserComparison cmp in context.UserComparisons.Include("User").Include("CompUser")
                    .Where(x => (x.UserId == requestData.UserId || x.CompUserId == requestData.UserId)).OrderByDescending(x => x.CreatedAt))
            {
                bool currentUserIsInitiator = true; //Initiator of past comparison
                if(cmp.CompUserId == requestData.UserId)
                {
                    string temp = cmp.UserId;
                    Account userTemp = cmp.User;
                    cmp.UserId = requestData.UserId;
                    cmp.CompUserId = temp;
                    cmp.User = cmp.CompUser;
                    cmp.CompUser = userTemp;
                    currentUserIsInitiator = false;
                }
                UserComparisonResultDTO added = result.FirstOrDefault(x => x.CompUserId == cmp.CompUserId);
                if (added == null)
                    result.Add(new UserComparisonResultDTO()
                    {
                        UserId = cmp.UserId,
                        CompUserId = cmp.CompUserId,
                        CompUserName = cmp.CompUser.Username,
                        EqualData = cmp.EqualJson,
                        Date = cmp.CreatedAt,
                        CurrentUserIsInitiator = currentUserIsInitiator
                    });
            }
            
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
