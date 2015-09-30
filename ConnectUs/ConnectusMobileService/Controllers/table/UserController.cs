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

namespace ConnectusMobileService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class UserController : TableController<Account>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Account>(context, Request, Services);
        }

        // GET tables/User
        public IQueryable<Account> GetAllUser()
        {
            return Query();
        }

        // GET tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Account> GetAccount(string id)
        {
            var result = Lookup(id).Queryable;

            return SingleResult<Account>.Create<Account>(result);
        }

        // PATCH tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Account> PatchAccount(string id, Delta<Account> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/User
        public async Task<IHttpActionResult> PostAccount(Account item)
        {
            Account current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAccount(string id)
        {
             return DeleteAsync(id);
        }

    }
}