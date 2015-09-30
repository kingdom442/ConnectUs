namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueconst : DbMigration
    {
        public override void Up()
        {
            CreateIndex("ConnectusMobileService.Accounts", "Username", unique: true, name: "UX_Username");
        }
        
        public override void Down()
        {
            DropIndex("ConnectusMobileService.Accounts", "UX_Username");
        }
    }
}
