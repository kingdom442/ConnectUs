namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ConnectusMobileService.Accounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Username = c.String(nullable: false, maxLength: 30),
                        Salt = c.Binary(),
                        SaltedAndHashedPassword = c.Binary(),
                        MailAddress = c.String(),
                        FacebookId = c.String(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"),
                        UpdatedAt = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CreatedAt, clustered: false);
            
            CreateTable(
                "ConnectusMobileService.Networks",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ConnectusMobileService.UserComparisons",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        CompUserId = c.String(maxLength: 128),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"),
                        UpdatedAt = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ConnectusMobileService.Accounts", t => t.CompUserId)
                .ForeignKey("ConnectusMobileService.Accounts", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CompUserId)
                .Index(t => t.CreatedAt, clustered: false);
            
            CreateTable(
                "ConnectusMobileService.UserContexts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Location = c.Geography(),
                        AccountRefId = c.String(maxLength: 128),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"),
                        UpdatedAt = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ConnectusMobileService.Accounts", t => t.AccountRefId)
                .Index(t => t.AccountRefId)
                .Index(t => t.CreatedAt, clustered: false);
            
            CreateTable(
                "ConnectusMobileService.UserInfoDetails",
                c => new
                    {
                        UserInfoId = c.String(nullable: false, maxLength: 128),
                        JsonInfo = c.String(),
                    })
                .PrimaryKey(t => t.UserInfoId)
                .ForeignKey("ConnectusMobileService.UserInfoes", t => t.UserInfoId)
                .Index(t => t.UserInfoId);
            
            CreateTable(
                "ConnectusMobileService.UserInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        NetworkId = c.Short(nullable: false),
                        ProfilePicUrl = c.String(maxLength: 2000),
                        Age = c.String(),
                        Gender = c.String(),
                        Description = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"),
                        UpdatedAt = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ConnectusMobileService.Accounts", t => t.UserId)
                .ForeignKey("ConnectusMobileService.Networks", t => t.NetworkId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.NetworkId)
                .Index(t => t.CreatedAt, clustered: false);
            
            CreateTable(
                "ConnectusMobileService.UserComparisonNetworks",
                c => new
                    {
                        UserComparison_Id = c.String(nullable: false, maxLength: 128),
                        Network_Id = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserComparison_Id, t.Network_Id })
                .ForeignKey("ConnectusMobileService.UserComparisons", t => t.UserComparison_Id, cascadeDelete: true)
                .ForeignKey("ConnectusMobileService.Networks", t => t.Network_Id, cascadeDelete: true)
                .Index(t => t.UserComparison_Id)
                .Index(t => t.Network_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ConnectusMobileService.UserInfoDetails", "UserInfoId", "ConnectusMobileService.UserInfoes");
            DropForeignKey("ConnectusMobileService.UserInfoes", "NetworkId", "ConnectusMobileService.Networks");
            DropForeignKey("ConnectusMobileService.UserInfoes", "UserId", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.UserContexts", "AccountRefId", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.UserComparisons", "UserId", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.UserComparisonNetworks", "Network_Id", "ConnectusMobileService.Networks");
            DropForeignKey("ConnectusMobileService.UserComparisonNetworks", "UserComparison_Id", "ConnectusMobileService.UserComparisons");
            DropForeignKey("ConnectusMobileService.UserComparisons", "CompUserId", "ConnectusMobileService.Accounts");
            DropIndex("ConnectusMobileService.UserComparisonNetworks", new[] { "Network_Id" });
            DropIndex("ConnectusMobileService.UserComparisonNetworks", new[] { "UserComparison_Id" });
            DropIndex("ConnectusMobileService.UserInfoes", new[] { "CreatedAt" });
            DropIndex("ConnectusMobileService.UserInfoes", new[] { "NetworkId" });
            DropIndex("ConnectusMobileService.UserInfoes", new[] { "UserId" });
            DropIndex("ConnectusMobileService.UserInfoDetails", new[] { "UserInfoId" });
            DropIndex("ConnectusMobileService.UserContexts", new[] { "CreatedAt" });
            DropIndex("ConnectusMobileService.UserContexts", new[] { "AccountRefId" });
            DropIndex("ConnectusMobileService.UserComparisons", new[] { "CreatedAt" });
            DropIndex("ConnectusMobileService.UserComparisons", new[] { "CompUserId" });
            DropIndex("ConnectusMobileService.UserComparisons", new[] { "UserId" });
            DropIndex("ConnectusMobileService.Accounts", new[] { "CreatedAt" });
            DropTable("ConnectusMobileService.UserComparisonNetworks");
            DropTable("ConnectusMobileService.UserInfoes");
            DropTable("ConnectusMobileService.UserInfoDetails");
            DropTable("ConnectusMobileService.UserContexts");
            DropTable("ConnectusMobileService.UserComparisons");
            DropTable("ConnectusMobileService.Networks");
            DropTable("ConnectusMobileService.Accounts");
        }
    }
}
