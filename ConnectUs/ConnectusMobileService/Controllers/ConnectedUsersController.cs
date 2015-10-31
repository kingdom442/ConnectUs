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
using ConnectusMobileService.Service.Data;

namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class ConnectedUsersController : ApiController
    {
        public ApiServices Services { get; set; }

        // POST api/ConnectedUsers
        public HttpResponseMessage Post(AccountId accountid)
        {
            MobileServiceContext context = new MobileServiceContext();
            if(accountid == null)
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            string requesterId = accountid.accountid;
            List<BasicUserInfoDTO> response = new List<BasicUserInfoDTO>();
            foreach (ConnectRequest connRequests in context.ConnectRequests.Where(x => x.Accepted == true && (x.RequestUserId == requesterId || x.ConnectUserId == requesterId)))
            {
                response.Add(UserInfoService.GetBasicUserInfo((connRequests.ConnectUserId != requesterId) ? connRequests.ConnectUserId: connRequests.RequestUserId));
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
