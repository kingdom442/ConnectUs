using System;
using System.Data.Entity;
using System.Web.Http;
using ConnectusMobileService.DataObjects;
using ConnectusMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Config;
using ConnectusMobileService.Utils.LoginProviders;

namespace ConnectusMobileService
{
    public static class WebApiConfig
    {
        
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();
            options.LoginProviders.Add(typeof(LinkedInLoginProvider));

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));
            //config.SetIsHosted(true);

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            //config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            //config.Properties["MS_IsHosted"] = true;

            //Database.SetInitializer<MobileServiceContext>(new MobileServiceInitializer());

            //Deployment
            Database.SetInitializer<MobileServiceContext>(null);

            //var migrator = new DbMigrator(new Configuration());
            //migrator.Update();
        }
    }

    //public class MobileServiceInitializer : ClearDatabaseSchemaIfModelChanges<MobileServiceContext>
    //{
    //    protected override void Seed(MobileServiceContext context)
    //    {
    //        byte[] salt = LoginProviderUtil.generateSalt();
    //        if (!context.Accounts.Any(a => a.Username == "max"))
    //        {
    //            Account defaultAccount1 = new Account
    //            {
    //                Id = Guid.NewGuid().ToString(),
    //                Username = "max",
    //                Salt = salt,
    //                SaltedAndHashedPassword = LoginProviderUtil.hash("password", salt)
    //            };
    //            context.Accounts.AddOrUpdate(defaultAccount1);
    //            UserInfo newUserInfo;
    //            context.UserInfos.AddOrUpdate(newUserInfo = new UserInfo()
    //            {
    //                Description = "About me",
    //                UserId = defaultAccount1.Id,
    //                Id = Guid.NewGuid().ToString(),
    //                NetworkId = (Int16)NetworkType.CONNECT_US,
    //                UserInfoDetail = new UserInfoDetail()
    //            });
    //        }
    //        if (!context.Accounts.Any(a => a.Username == "user"))
    //        {
    //            Account defaultAccount2 = new Account
    //            {
    //                Id = Guid.NewGuid().ToString(),
    //                Username = "user",
    //                Salt = salt,
    //                SaltedAndHashedPassword = LoginProviderUtil.hash("password", salt)
    //            };
    //            context.Accounts.AddOrUpdate(defaultAccount2);
    //            UserInfo defaultUserInfo2;
    //            context.UserInfos.AddOrUpdate(defaultUserInfo2 = new UserInfo()
    //            {
    //                Description = "About me",
    //                UserId = defaultAccount2.Id,
    //                Id = Guid.NewGuid().ToString(),
    //                NetworkId = (Int16)NetworkType.CONNECT_US,
    //                UserInfoDetail = new UserInfoDetail()
    //            });
    //        }
    //        if (context.Networks.Count() == 0)
    //            Network.GetAllNetworks().ToList().ForEach(n => context.Networks.AddOrUpdate(n));

    //        context.Database.ExecuteSqlCommand("delete from ConnectusMobileService.Events");
    //        Event defaultEvent1 = new Event()
    //        {
    //            Id = Guid.NewGuid().ToString(),
    //            Name = "Defaultevent",
    //            Place = "Linz",
    //            FromDate = new DateTimeOffset(2015, 8, 1, 12, 0, 0, TimeSpan.FromHours(0)),
    //            ToDate = new DateTimeOffset(2015, 11, 1, 12, 0, 0, TimeSpan.FromHours(0))
    //        };
    //        context.Events.Add(defaultEvent1);
    //        Event defaultEvent2 = new Event()
    //        {
    //            Id = Guid.NewGuid().ToString(),
    //            Name = "Upcoming Event",
    //            Place = "Wien",
    //            FromDate = new DateTimeOffset(2015, 11, 5, 12, 0, 0, TimeSpan.FromHours(0)),
    //            ToDate = new DateTimeOffset(2015, 12, 1, 12, 0, 0, TimeSpan.FromHours(0))
    //        };
    //        context.Events.Add(defaultEvent2);


    //        context.SaveChanges();
    //        base.Seed(context);
    //    }
    //}
}

