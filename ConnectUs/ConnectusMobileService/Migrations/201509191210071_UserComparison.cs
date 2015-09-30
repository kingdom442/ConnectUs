namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserComparison : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ConnectusMobileService.UserComparisons", "CompUserId", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.UserComparisons", "UserId", "ConnectusMobileService.Accounts");
            DropIndex("ConnectusMobileService.UserComparisons", new[] { "UserId" });
            DropIndex("ConnectusMobileService.UserComparisons", new[] { "CompUserId" });
            AddColumn("ConnectusMobileService.UserComparisons", "EqualJson", c => c.String());
            AddColumn("ConnectusMobileService.UserComparisons", "OnlyUserJson", c => c.String());
            AddColumn("ConnectusMobileService.UserComparisons", "OnlyCompUserJson", c => c.String());
            AlterColumn("ConnectusMobileService.UserComparisons", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("ConnectusMobileService.UserComparisons", "CompUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("ConnectusMobileService.UserComparisons", "UserId");
            CreateIndex("ConnectusMobileService.UserComparisons", "CompUserId");
            AddForeignKey("ConnectusMobileService.UserComparisons", "CompUserId", "ConnectusMobileService.Accounts", "Id", cascadeDelete: false);
            AddForeignKey("ConnectusMobileService.UserComparisons", "UserId", "ConnectusMobileService.Accounts", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("ConnectusMobileService.UserComparisons", "UserId", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.UserComparisons", "CompUserId", "ConnectusMobileService.Accounts");
            DropIndex("ConnectusMobileService.UserComparisons", new[] { "CompUserId" });
            DropIndex("ConnectusMobileService.UserComparisons", new[] { "UserId" });
            AlterColumn("ConnectusMobileService.UserComparisons", "CompUserId", c => c.String(maxLength: 128));
            AlterColumn("ConnectusMobileService.UserComparisons", "UserId", c => c.String(maxLength: 128));
            DropColumn("ConnectusMobileService.UserComparisons", "OnlyCompUserJson");
            DropColumn("ConnectusMobileService.UserComparisons", "OnlyUserJson");
            DropColumn("ConnectusMobileService.UserComparisons", "EqualJson");
            CreateIndex("ConnectusMobileService.UserComparisons", "CompUserId");
            CreateIndex("ConnectusMobileService.UserComparisons", "UserId");
            AddForeignKey("ConnectusMobileService.UserComparisons", "UserId", "ConnectusMobileService.Accounts", "Id");
            AddForeignKey("ConnectusMobileService.UserComparisons", "CompUserId", "ConnectusMobileService.Accounts", "Id");
        }
    }
}
