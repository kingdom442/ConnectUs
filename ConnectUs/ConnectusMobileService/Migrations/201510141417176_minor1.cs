namespace ConnectusMobileService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minor1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ConnectusMobileService.UserContacts", "Id", "ConnectusMobileService.Accounts");
            DropIndex("ConnectusMobileService.UserContacts", new[] { "Id" });
            AddColumn("ConnectusMobileService.UserContacts", "UserId", c => c.String(maxLength: 128));
            CreateIndex("ConnectusMobileService.UserContacts", "UserId", unique: true);
            AddForeignKey("ConnectusMobileService.UserContacts", "UserId", "ConnectusMobileService.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("ConnectusMobileService.UserContacts", "UserId", "ConnectusMobileService.Accounts");
            DropIndex("ConnectusMobileService.UserContacts", new[] { "UserId" });
            DropColumn("ConnectusMobileService.UserContacts", "UserId");
            CreateIndex("ConnectusMobileService.UserContacts", "Id");
            AddForeignKey("ConnectusMobileService.UserContacts", "Id", "ConnectusMobileService.Accounts", "Id");
        }
    }
}
