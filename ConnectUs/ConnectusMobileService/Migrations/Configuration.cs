namespace ConnectusMobileService.Migrations
{
    using Utils;
    using Microsoft.WindowsAzure.Mobile.Service.Tables;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataObjects;

    internal sealed class Configuration : DbMigrationsConfiguration<ConnectusMobileService.Models.MobileServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new EntityTableSqlGenerator());
        }

        protected override void Seed(ConnectusMobileService.Models.MobileServiceContext context)
        {
            byte[] salt = LoginProviderUtil.generateSalt();
            if (!context.Accounts.Any(a => a.Username == "max"))
            {
                Account defaultAccount1 = new Account
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "max",
                    Salt = salt,
                    SaltedAndHashedPassword = LoginProviderUtil.hash("password", salt)
                };
                context.Accounts.AddOrUpdate(defaultAccount1);
                UserInfo newUserInfo;
                context.UserInfos.AddOrUpdate(newUserInfo = new UserInfo()
                {
                    Description = "About me",
                    UserId = defaultAccount1.Id,
                    Id = Guid.NewGuid().ToString(),
                    NetworkId = (Int16)NetworkType.CONNECT_US,
                    UserInfoDetail = new UserInfoDetail()
                });
            }
            if (!context.Accounts.Any(a => a.Username == "user"))
            {
                Account defaultAccount2 = new Account
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "user",
                    Salt = salt,
                    SaltedAndHashedPassword = LoginProviderUtil.hash("password", salt)
                };
                context.Accounts.AddOrUpdate(defaultAccount2);
                UserInfo defaultUserInfo2;
                context.UserInfos.AddOrUpdate(defaultUserInfo2 = new UserInfo()
                {
                    Description = "About me",
                    UserId = defaultAccount2.Id,
                    Id = Guid.NewGuid().ToString(),
                    NetworkId = (Int16)NetworkType.CONNECT_US,
                    UserInfoDetail = new UserInfoDetail()
                });
            }
            if(context.Networks.Count() == 0)
                Network.GetAllNetworks().ToList().ForEach(n => context.Networks.AddOrUpdate(n));

            Event defaultEvent1 = new Event()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Defaultevent",
                Place = "Linz",
                FromDate = new DateTimeOffset(2015, 8, 1, 12, 0, 0, TimeSpan.FromHours(0)),
                ToDate = new DateTimeOffset(2015, 11, 1, 12, 0, 0, TimeSpan.FromHours(0))
            };
            context.Events.Add(defaultEvent1);
            Event defaultEvent2 = new Event()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Upcoming Event",
                Place = "Wien",
                FromDate = new DateTimeOffset(2015, 11, 5, 12, 0, 0, TimeSpan.FromHours(0)),
                ToDate = new DateTimeOffset(2015, 12, 1, 12, 0, 0, TimeSpan.FromHours(0))
            };
            context.Events.Add(defaultEvent2);


            context.SaveChanges();
            base.Seed(context);
        }
    }
}
