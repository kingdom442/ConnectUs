namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minor2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("ConnectusMobileService.UserComparisons", "UX_UseComparison");
            CreateIndex("ConnectusMobileService.UserComparisons", new[] { "UserId", "CompUserId", "CreatedAt" }, unique: true, clustered: false, name: "UX_UseComparison");
            DropColumn("ConnectusMobileService.UserComparisons", "IsNewestComparison");
        }
        
        public override void Down()
        {
            AddColumn("ConnectusMobileService.UserComparisons", "IsNewestComparison", c => c.Boolean(nullable: false));
            DropIndex("ConnectusMobileService.UserComparisons", "UX_UseComparison");
            CreateIndex("ConnectusMobileService.UserComparisons", new[] { "UserId", "CompUserId", "IsNewestComparison" }, unique: true, name: "UX_UseComparison");
        }
    }
}
