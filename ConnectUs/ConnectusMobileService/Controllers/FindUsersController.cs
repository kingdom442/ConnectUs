using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using ConnectusMobileService.DataObjects;
using System.Data.Entity.Spatial;
using ConnectusMobileService.Models;
using ConnectusMobileService.DataObjects.DTO;
using ConnectusMobileService.Utils;
using Microsoft.WindowsAzure.MobileServices;
using ConnectusMobileService.Service.Data;

namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class FindUsersController : ApiController
    {
        public ApiServices Services { get; set; }

        // POST api/FindUsers
        public HttpResponseMessage Post(FindUsersRequestData requestData)
        {
            MobileServiceContext context = new MobileServiceContext();
            Account requester = context.Accounts.FirstOrDefault(x => x.Id == requestData.UserId);
            if (requester == null)
            {
                Services.Log.Error("User with id from param is not available");
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            bool checkLocation = requestData.MaxDistance > 0 && (requestData.Latitude != 0 || requestData.Longitude != 0);

            if (checkLocation)
            {
                DateTimeOffset fiveHoursBefore = DateTimeOffset.Now.AddHours(-500);
                List<UserContext> uContexts = context.UserContexts.ToList();
                uContexts = uContexts.Where(x => DateTimeOffset.Compare(x.CreatedAt.Value, fiveHoursBefore) > 0).GroupBy(uContext => uContext.AccountRefId).Select(grp => grp.First()).OrderBy(x => x.CreatedAt).ToList();
                uContexts.RemoveAll(ctx => ctx.AccountRefId == requestData.UserId);
                uContexts.RemoveAll(ctx => !((requester.BusinessInterest && ctx.Account.BusinessInterest) || (requester.PrivateInterest && ctx.Account.PrivateInterest)));
                if (requestData.AlreadyCompared.HasValue)
                {
                    uContexts.RemoveAll(ctx =>
                    {
                        return !requestData.AlreadyCompared.Value == context.UserComparisons.Any(comp => (comp.UserId == requestData.UserId || comp.UserId == ctx.AccountRefId) && (comp.CompUserId == requestData.UserId || comp.CompUserId == ctx.AccountRefId));
                    });

                }

                DbGeography georaphy = DBGeographyUtil.getDBGeography(requestData.Longitude, requestData.Latitude);
                uContexts.RemoveAll(ctx => ctx.Location.Distance(georaphy) > requestData.MaxDistance);
                
                List<BasicUserInfoDTO> response = uContexts.Select(ucont =>
                {
                    try
                    {

                        BasicUserInfoDTO userInfo = UserInfoService.GetBasicUserInfo(ucont.AccountRefId);
                        return userInfo;
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                        return null;
                    }

                }
                ).ToList();

                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                List<BasicUserInfoDTO> response = new List<BasicUserInfoDTO>();
                List<UserComparison> allComparisons = context.UserComparisons.ToList();
                foreach (Account a in context.Accounts.Where(a => a.Id != requestData.UserId))
                {
                    if (!requestData.AlreadyCompared.HasValue || requestData.AlreadyCompared.Value == allComparisons.Any(comp => (comp.UserId == requestData.UserId || comp.UserId == a.Id) && (comp.CompUserId == requestData.UserId || comp.CompUserId == a.Id)))
                        if ((requester.BusinessInterest && a.BusinessInterest) || (requester.PrivateInterest && a.PrivateInterest))
                        response.Add(UserInfoService.GetBasicUserInfo(a.Id));
                }
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

    }
}
