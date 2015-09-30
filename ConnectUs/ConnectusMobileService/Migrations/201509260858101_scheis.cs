namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scheis : DbMigration
    {
        public override void Up()
        {
            DropIndex("ConnectusMobileService.Accounts", "UX_FacebookId");
        }
        
        public override void Down()
        {
            CreateIndex("ConnectusMobileService.Accounts", "FacebookId", unique: true, name: "UX_FacebookId");
        }
    }
}
