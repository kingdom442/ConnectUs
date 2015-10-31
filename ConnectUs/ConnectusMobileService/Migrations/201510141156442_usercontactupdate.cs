namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usercontactupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ConnectusMobileService.EventAccounts", "Event_Id", "ConnectusMobileService.Events");
            DropForeignKey("ConnectusMobileService.EventAccounts", "Account_Id", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.UserContacts", "Id", "ConnectusMobileService.Accounts");
            DropIndex("ConnectusMobileService.EventAccounts", new[] { "Event_Id" });
            DropIndex("ConnectusMobileService.EventAccounts", new[] { "Account_Id" });
            AddColumn("ConnectusMobileService.Accounts", "Event_Id", c => c.String(maxLength: 128));
            AddColumn("ConnectusMobileService.Accounts", "UserContact_Id", c => c.String(maxLength: 128));
            AddColumn("ConnectusMobileService.Events", "Account_Id", c => c.String(maxLength: 128));
            AddColumn("ConnectusMobileService.UserContacts", "Version", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("ConnectusMobileService.UserContacts", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("ConnectusMobileService.UserContacts", "UpdatedAt", c => c.DateTimeOffset(precision: 7));
            AddColumn("ConnectusMobileService.UserContacts", "Deleted", c => c.Boolean(nullable: false));
            CreateIndex("ConnectusMobileService.Accounts", "Event_Id");
            CreateIndex("ConnectusMobileService.Accounts", "UserContact_Id");
            CreateIndex("ConnectusMobileService.Events", "Account_Id");
            CreateIndex("ConnectusMobileService.UserContacts", "CreatedAt", clustered: false);
            AddForeignKey("ConnectusMobileService.Accounts", "Event_Id", "ConnectusMobileService.Events", "Id");
            AddForeignKey("ConnectusMobileService.Events", "Account_Id", "ConnectusMobileService.Accounts", "Id");
            AddForeignKey("ConnectusMobileService.Accounts", "UserContact_Id", "ConnectusMobileService.Events", "Id");
            DropTable("ConnectusMobileService.EventAccounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "ConnectusMobileService.EventAccounts",
                c => new
                    {
                        Event_Id = c.String(nullable: false, maxLength: 128),
                        Account_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Event_Id, t.Account_Id });
            
            DropForeignKey("ConnectusMobileService.Accounts", "UserContact_Id", "ConnectusMobileService.Events");
            DropForeignKey("ConnectusMobileService.Events", "Account_Id", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.Accounts", "Event_Id", "ConnectusMobileService.Events");
            DropIndex("ConnectusMobileService.UserContacts", new[] { "CreatedAt" });
            DropIndex("ConnectusMobileService.Events", new[] { "Account_Id" });
            DropIndex("ConnectusMobileService.Accounts", new[] { "UserContact_Id" });
            DropIndex("ConnectusMobileService.Accounts", new[] { "Event_Id" });
            DropColumn("ConnectusMobileService.UserContacts", "Deleted");
            DropColumn("ConnectusMobileService.UserContacts", "UpdatedAt");
            DropColumn("ConnectusMobileService.UserContacts", "CreatedAt");
            DropColumn("ConnectusMobileService.UserContacts", "Version");
            DropColumn("ConnectusMobileService.Events", "Account_Id");
            DropColumn("ConnectusMobileService.Accounts", "UserContact_Id");
            DropColumn("ConnectusMobileService.Accounts", "Event_Id");
            CreateIndex("ConnectusMobileService.EventAccounts", "Account_Id");
            CreateIndex("ConnectusMobileService.EventAccounts", "Event_Id");
            AddForeignKey("ConnectusMobileService.UserContacts", "Id", "ConnectusMobileService.Accounts", "Id");
            AddForeignKey("ConnectusMobileService.EventAccounts", "Account_Id", "ConnectusMobileService.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("ConnectusMobileService.EventAccounts", "Event_Id", "ConnectusMobileService.Events", "Id", cascadeDelete: true);
        }
    }
}
