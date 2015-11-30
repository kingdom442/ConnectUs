using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using ConnectusMobileService.DataObjects.DTO;
using ConnectusMobileService.Models;
using ConnectusMobileService.DataObjects;

namespace ConnectusMobileService.Controllers
{
    public class EqualConnectionsController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/EqualConnections
        public HttpResponseMessage Post(UserComparisonRequestDTO parameters)
        {
            Services.Log.Info("EqualConnectionsController entered!");
            MobileServiceContext context = new MobileServiceContext();
            List<BasicUserInfoDTO> equal = new List<BasicUserInfoDTO>();

            List<Account> userConnections = context.ConnectRequests.Where(cr => cr.RequestUserId == parameters.UserId || cr.ConnectUserId == parameters.UserId).Select(cr => ((cr.ConnectUserId == parameters.UserId) ? cr.RequestUser : cr.ConnectUser)).ToList();
            List<Account> user2Connections = context.ConnectRequests.Where(cr => cr.RequestUserId == parameters.CompUserId || cr.ConnectUserId == parameters.CompUserId).Select(cr => ((cr.ConnectUserId == parameters.CompUserId) ? cr.RequestUser : cr.ConnectUser)).ToList();

            IEnumerable<Account> intersect = userConnections.Intersect<Account>(user2Connections);

            foreach (Account account in intersect)
            {
                if (account != null)
                {
                    BasicUserInfoDTO userInfo = new BasicUserInfoDTO();
                    userInfo.AccountId = account.Id;
                    UserInfo ui = context.UserInfos.First(uinfo => uinfo.UserId == account.Id);
                    userInfo.Age = ui.Age;
                    userInfo.Status = ui.About;
                    userInfo.Username = account.Username;
                    userInfo.FirstName = ui.FirstName;
                    userInfo.LastName = ui.LastName;
                    userInfo.ProfilePicUrl = ui.ProfilePicUrl;
                    equal.Add(userInfo);
                }
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, equal);
        }

    }
}
