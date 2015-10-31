using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.Models;
using ConnectusMobileService.DataObjects.DTO;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using System.Data.Entity.Spatial;
using ConnectusMobileService.Utils;

namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class UserContactController : TableController<UserContact>
    {
        MobileServiceContext context = new MobileServiceContext();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DomainManager = new EntityDomainManager<UserContact>(context, Request, Services);
        }

        // GET tables/UserContext
        public IQueryable<UserContact> GetAllUserContext()
        {
            return Query();
        }

        // GET tables/UserContext/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<UserContact> GetUserContext(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/UserContext/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<UserContact> PatchUserContext(string id, Delta<UserContact> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/UserContext
        public async Task<IHttpActionResult> PostUserContext(UserContact item)
        {
            UserContact current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }
        
    }
}