using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;
using ConnectusMobileService.DataObjects;

namespace ConnectusMobileService.Models
{

    public class MobileServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.

        private const string connectionStringName = "MS_TableConnectionString";

        public MobileServiceContext() : base(connectionStringName)
        {

        }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserContext> UserContexts { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserInfoDetail> UserInfoDetails { get; set; }
        public DbSet<UserComparison> UserComparisons { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ConnectRequest> ConnectRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = ServiceSettingsDictionary.GetSchemaName();
            if (!string.IsNullOrEmpty(schema))
            {
                modelBuilder.HasDefaultSchema(schema);
            }

        }
    }

}
