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
using ConnectusMobileService.Utils.Singleton;

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
            DateTimeOffset fiveHoursBefore = DateTimeOffset.Now.AddHours(-500);
            List<UserContext> uContexts = context.UserContexts.ToList();
            uContexts = uContexts.Where(x => DateTimeOffset.Compare(x.CreatedAt.Value, fiveHoursBefore) > 0).GroupBy(uContext => uContext.AccountRefId).Select(grp => grp.First()).OrderBy(x => x.CreatedAt).ToList();
            uContexts.RemoveAll(ctx => ctx.AccountRefId == requestData.UserId);
            if (requestData.AlreadyCompared.HasValue)
            {
                uContexts.RemoveAll(ctx =>
                {
                    return !requestData.AlreadyCompared.Value == context.UserComparisons.Any(comp => (comp.UserId == requestData.UserId || comp.UserId == ctx.AccountRefId) && (comp.CompUserId == requestData.UserId || comp.CompUserId == ctx.AccountRefId));
                });
            }
            DbGeography georaphy = DBGeographyUtil.getDBGeography(requestData.Longitude, requestData.Latitude);
            //uContexts.RemoveAll(ctx => ctx.Location.Distance(georaphy) > requestData.MaxDistance);
            List<BasicUserInfoDTO> response = uContexts.Select(ucont =>
            {
                try
                {
                    BasicUserInfoDTO userInfo = new BasicUserInfoDTO();
                    userInfo.AccountId = ucont.AccountRefId;
                    foreach (UserInfo ui in context.UserInfos.Include("Account").Where(ui => ui.UserId == ucont.AccountRefId).OrderBy(x => x.Network.Id))
                    {
                        userInfo.Age = (userInfo.Age == null) ? ui.Age : userInfo.Age;
                        userInfo.Description = (userInfo.Description == null) ? ui.Description : userInfo.Description;
                        userInfo.FacebookConnected = (userInfo.FacebookConnected || ui.NetworkId == (Int16)NetworkType.FACEBOOK) ? true : false;
                        userInfo.Gender = (userInfo.Gender == null) ? ui.Gender : userInfo.Gender;
                        userInfo.Username = (userInfo.Username == null) ? ui.Account.Username : userInfo.Username;
                        userInfo.FirstName = (userInfo.FirstName == null) ? ui.FirstName : userInfo.FirstName;
                        userInfo.LastName = (userInfo.LastName == null) ? ui.LastName : userInfo.LastName;
                        userInfo.ProfilePicUrl = (userInfo.ProfilePicUrl == null) ? ui.ProfilePicUrl : userInfo.ProfilePicUrl;
                    }
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

    }
}
