namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unique_connect_request : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ConnectusMobileService.ConnectRequests",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RequestUserId = c.String(maxLength: 128),
                        ConnectUserId = c.String(maxLength: 128),
                        Accepted = c.Boolean(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"),
                        UpdatedAt = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ConnectusMobileService.Accounts", t => t.ConnectUserId)
                .ForeignKey("ConnectusMobileService.Accounts", t => t.RequestUserId)
                .Index(t => new { t.RequestUserId, t.ConnectUserId }, unique: true, name: "UX_ConnectRequest")
                .Index(t => t.CreatedAt, clustered: false);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ConnectusMobileService.ConnectRequests", "RequestUserId", "ConnectusMobileService.Accounts");
            DropForeignKey("ConnectusMobileService.ConnectRequests", "ConnectUserId", "ConnectusMobileService.Accounts");
            DropIndex("ConnectusMobileService.ConnectRequests", new[] { "CreatedAt" });
            DropIndex("ConnectusMobileService.ConnectRequests", "UX_ConnectRequest");
            DropTable("ConnectusMobileService.ConnectRequests");
        }
    }
}
