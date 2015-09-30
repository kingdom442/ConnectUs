namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _269 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ConnectusMobileService.Accounts", "FacebookId", c => c.String(maxLength: 30));
        }
        public override void Down()
        {
            AlterColumn("ConnectusMobileService.Accounts", "FacebookId", c => c.String());
        }
    }
}
