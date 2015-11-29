namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class username : DbMigration
    {
        public override void Up()
        {
            DropIndex("connectus.Accounts", "UX_Username");
        }
        
        public override void Down()
        {
            CreateIndex("connectus.Accounts", "Username", unique: true, name: "UX_Username");
        }
    }
}
