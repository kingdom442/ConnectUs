using System;
using System.Data.Entity;
using System.Web.Http;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using ConnectusMobileService.Utils;
using System.Data.Entity.Migrations;
using Microsoft.WindowsAzure.Mobile.Service.Config;

namespace ConnectusMobileService
{
    public class WebApiConfig: IBootstrapper
    {
        public void Initialize()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));
            //config.SetIsHosted(true);

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            //config.Properties["MS_IsHosted"] = true;
            
            Database.SetInitializer<MobileServiceContext>(new MobileServiceInitializer());
      
            //var migrator = new DbMigrator(new Configuration());
            //migrator.Update();
        }
    }

    public class MobileServiceInitializer : ClearDatabaseSchemaIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            byte[] salt = LoginProviderUtil.generateSalt();
            Account defaultAccount1 = new Account
            {
                Id = Guid.NewGuid().ToString(),
                Username = "max",
                Salt = salt,
                SaltedAndHashedPassword = LoginProviderUtil.hash("password", salt)
            };
            context.Accounts.AddOrUpdate(defaultAccount1);
            Account defaultAccount2 = new Account
            {
                Id = Guid.NewGuid().ToString(),
                Username = "user",
                Salt = salt,
                SaltedAndHashedPassword = LoginProviderUtil.hash("password", salt)
            };
            context.Accounts.AddOrUpdate(defaultAccount2);


            foreach (Network network in Network.GetAllNetworks())
            {
               context.Networks.AddOrUpdate(network);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}

