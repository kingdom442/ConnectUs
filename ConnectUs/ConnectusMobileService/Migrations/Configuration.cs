namespace ConnectusMobileService.Migrations
{
    using ConnectusMobileService.Utils.LoginProviders;
    using Microsoft.WindowsAzure.Mobile.Service.Tables;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataObjects;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ConnectusMobileService.Models.MobileServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = "Migrations";
            SetSqlGenerator("System.Data.SqlClient", new AzureSqlGenerator());
        }

        protected override void Seed(ConnectusMobileService.Models.MobileServiceContext context)
        {
            byte[] salt = LoginProviderUtil.generateSalt();
            if (!context.Accounts.Any(a => a.Username == "max"))
            {
                Account defaultAccount1 = new Account()
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
                    Bio = "About me",
                    UserId = defaultAccount1.Id,
                    Id = Guid.NewGuid().ToString()
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
                    Bio = "About me",
                    UserId = defaultAccount2.Id,
                    Id = Guid.NewGuid().ToString()
                });
            }
            if(context.Networks.Count() == 0)
                Network.GetAllNetworks().ToList().ForEach(n => context.Networks.AddOrUpdate(n));

            if (!context.Events.Any(e => e.Name == "Defaultevent"))
            {
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
            }
            AddTestUsers(context);
            context.SaveChanges();
            base.Seed(context);
        }

        private void AddTestUsers(MobileServiceContext context)
        {
            if (!context.Accounts.Any(e => e.Username == "Maxi"))
            {
                Account testAccount1 = new Account
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "Maxi"
                };
                context.Accounts.AddOrUpdate(testAccount1);
                UserInfo testUserInfo1;
                context.UserInfos.AddOrUpdate(testUserInfo1 = new UserInfo()
                {
                    Bio = "Test",
                    About = "Ich bin der Testmaxi",
                    UserId = testAccount1.Id,
                    Id = Guid.NewGuid().ToString()
                });
            }
        }

    }
}
