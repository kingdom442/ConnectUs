namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ConnectusMobileService.Events", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("ConnectusMobileService.Events", "Place", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("ConnectusMobileService.Events", "Place", c => c.String(nullable: false));
            AlterColumn("ConnectusMobileService.Events", "Name", c => c.String(nullable: false));
        }
    }
}
