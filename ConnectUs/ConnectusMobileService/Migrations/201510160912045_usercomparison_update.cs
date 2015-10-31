namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usercomparison_update : DbMigration
    {
        public override void Up()
        {
            DropIndex("ConnectusMobileService.UserComparisons", new[] { "UserId" });
            DropIndex("ConnectusMobileService.UserComparisons", new[] { "CompUserId" });
            AddColumn("ConnectusMobileService.UserComparisons", "IsNewestComparison", c => c.Boolean(nullable: false));
            CreateIndex("ConnectusMobileService.UserComparisons", new[] { "UserId", "CompUserId", "IsNewestComparison" }, unique: true, name: "UX_UseComparison");
        }
        
        public override void Down()
        {
            DropIndex("ConnectusMobileService.UserComparisons", "UX_UseComparison");
            DropColumn("ConnectusMobileService.UserComparisons", "IsNewestComparison");
            CreateIndex("ConnectusMobileService.UserComparisons", "CompUserId");
            CreateIndex("ConnectusMobileService.UserComparisons", "UserId");
        }
    }
}
