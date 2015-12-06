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
    public class UserContextController : TableController<UserContext>
    {
        MobileServiceContext context = new MobileServiceContext();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DomainManager = new EntityDomainManager<UserContext>(context, Request, Services);
        }

        // GET tables/UserContext
        public IQueryable<UserContext> GetAllUserContext()
        {
            return Query(); 
        }

        // GET tables/UserContext/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<UserContext> GetUserContext(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/UserContext/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<UserContext> PatchUserContext(string id, Delta<UserContext> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/UserContext
        public async Task<IHttpActionResult> PostUserContext(UserContextDTO uContext)
        {
            var count = context.Accounts.Where(x => x.Id == uContext.UserId).Count();
            if (count < 1)
            {
                return BadRequest();
            }
            if (uContext.Longitude < -90 || uContext.Longitude > 90)
                uContext.Longitude = 10;
            if (uContext.Latitude < -90 || uContext.Latitude > 90)
                uContext.Latitude = 10;
            DbGeography location = DBGeographyUtil.getDBGeography(uContext.Longitude, uContext.Latitude);
            UserContext newUserContext = new UserContext()
            {
                Account = context.Accounts.Where(x => x.Id == uContext.UserId).SingleOrDefault(),
                Location = location
            };
            UserContext current = await InsertAsync(newUserContext);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }
        
    }
}