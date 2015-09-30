using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using ConnectusMobileService.DataObjects.DTO;

namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class CheckAuthTokenController : ApiController
    {
        public ApiServices Services { get; set; }

        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
