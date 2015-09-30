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

            Network.GetAllNetworks().ToList().ForEach(n => context.Networks.AddOrUpdate(n));
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
