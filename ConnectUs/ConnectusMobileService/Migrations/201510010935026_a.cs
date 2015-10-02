namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ConnectusMobileService.ConnectRequests", "Accepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("ConnectusMobileService.ConnectRequests", "Accepted", c => c.Boolean(nullable: false));
        }
    }
}
