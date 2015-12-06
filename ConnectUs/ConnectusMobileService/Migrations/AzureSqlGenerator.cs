using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.Migrations
{
    public class AzureSqlGenerator: SqlServerMigrationSqlGenerator
    {
        protected override void Generate(CreateTableOperation createTableOperation)
        {
            if ((createTableOperation.PrimaryKey != null)
           && !createTableOperation.PrimaryKey.IsClustered)
            {
                createTableOperation.PrimaryKey.IsClustered = true;
            }

            base.Generate(createTableOperation);
        }
    }
}