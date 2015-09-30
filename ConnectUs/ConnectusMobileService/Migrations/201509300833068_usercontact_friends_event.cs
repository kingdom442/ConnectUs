namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usercontact_friends_event : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ConnectusMobileService.Events",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Place = c.String(nullable: false),
                        Location = c.Geography(),
                        FromDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ToDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"),
                        UpdatedAt = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CreatedAt, clustered: false);
            
            CreateTable(
                "ConnectusMobileService.UserContacts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PhoneNr = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ConnectusMobileService.Accounts", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "ConnectusMobileService.EventAccounts",
                c => new
                    {
                        Event_Id = c.String(nullable: false, maxLength: 128),
                        Account_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Event_Id, t.Account_Id })
                .ForeignKey("ConnectusMobileService.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("ConnectusMobileService.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Event_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ConnectusMobileService.UserContacts", "Id", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.EventAccounts", "Account_Id", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.EventAccounts", "Event_Id", "ConnectusMobileService.Events");
            DropIndex("ConnectusMobileService.EventAccounts", new[] { "Account_Id" });
            DropIndex("ConnectusMobileService.EventAccounts", new[] { "Event_Id" });
            DropIndex("ConnectusMobileService.UserContacts", new[] { "Id" });
            DropIndex("ConnectusMobileService.Events", new[] { "CreatedAt" });
            DropTable("ConnectusMobileService.EventAccounts");
            DropTable("ConnectusMobileService.UserContacts");
            DropTable("ConnectusMobileService.Events");
        }
    }
}
