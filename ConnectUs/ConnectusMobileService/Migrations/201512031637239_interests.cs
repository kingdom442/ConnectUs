namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class interests : DbMigration
    {
        public override void Up()
        {
            AddColumn("connectus.Accounts", "BusinessInterest", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("connectus.Accounts", "PrivateInterest", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("connectus.Accounts", "PrivateInterest");
            DropColumn("connectus.Accounts", "BusinessInterest");
        }
    }
}
