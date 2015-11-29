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
    public class GetEventParticipantsController : ApiController
    {
        public ApiServices Services { get; set; }

        // POST api/ParticipateEvent
        public HttpResponseMessage Post(ParticipateEventRequest requestData)
        {
            MobileServiceContext context = new MobileServiceContext();
            if(requestData == null ||requestData.eventid == null)
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            Event evt = context.Events.Include("Accounts").FirstOrDefault(e => e.Id == requestData.eventid);
            List<BasicUserInfoDTO> participators = new List<BasicUserInfoDTO>();
            foreach(Account a in evt.Accounts)
            {
                participators.Add(UserInfoService.GetBasicUserInfo(a.Id));                
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, participators);
        }
    }
}
