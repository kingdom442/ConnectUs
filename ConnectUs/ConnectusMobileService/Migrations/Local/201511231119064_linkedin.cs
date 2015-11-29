namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkedin : DbMigration
    {
        public override void Up()
        {
            AddColumn("connectus.Accounts", "LinkedInId", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("connectus.Accounts", "LinkedInId");
        }
    }
}
