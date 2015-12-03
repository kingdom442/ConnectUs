namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventdesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("connectus.Events", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("connectus.Events", "Description");
        }
    }
}
