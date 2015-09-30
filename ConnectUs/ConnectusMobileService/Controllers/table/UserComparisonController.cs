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
    public class UserComparisonController : TableController<UserComparison>
    {
        MobileServiceContext context = new MobileServiceContext();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DomainManager = new EntityDomainManager<UserComparison>(context, Request, Services);
        }

        // GET tables/UserContext
        public IQueryable<UserComparisonResultDTO> GetAllUserContext()
        {
            return Query().Select(x => new UserComparisonResultDTO()
            {
                UserId = x.UserId,
                UserName = x.User.Username,
                CompUserId = x.CompUserId,
                CompUserName = x.CompUser.Username,
                Date = x.CreatedAt,
                EqualData = x.EqualJson
            }); 
        }

        // GET tables/UserContext/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<UserComparison> GetUserContext(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/UserContext/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<UserComparison> PatchUserContext(string id, Delta<UserComparison> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/UserContext
        public async Task<IHttpActionResult> PostUserContext(UserComparison item)
        {
            UserComparison current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }
        
    }
}