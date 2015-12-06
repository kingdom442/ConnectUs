using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.Models;
using System;

namespace ConnectusMobileService.Controllers
{
    public class UserInfoController : TableController<UserInfo>
    {
        private readonly MobileServiceContext context = new MobileServiceContext();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
           
            DomainManager = new EntityDomainManager<UserInfo>(context, Request, Services);
        }

        // GET tables/UserInfo
        public IQueryable<UserInfo> GetAllUserInfo()
        {
            return Query(); 
        }

        // GET tables/UserInfo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<UserInfo> GetUserInfo(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/UserInfo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<UserInfo> PatchUserInfo(string id, Delta<UserInfo> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/UserInfo
        public async Task<IHttpActionResult> PostUserInfo(UserInfo item)
        {
            try { 
                UserInfo current = await InsertAsync(item);
                return CreatedAtRoute("Tables", new { id = current.Id }, current);
            }catch(Exception e)
            {
                Services.Log.Error(e.Message);
                return null;
            }
        }

        // DELETE tables/UserInfo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUserInfo(string id)
        {
             return DeleteAsync(id);
        }

    }
}