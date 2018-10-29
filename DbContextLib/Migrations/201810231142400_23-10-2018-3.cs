namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _231020183 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AddressWithIds", "VerificationRequestedByUserId", "dbo.AspNetUsers");
            DropIndex("dbo.AddressWithIds", new[] { "VerificationRequestedByUserId" });
            DropIndex("dbo.AddressWithIds", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AddressWithIds", "UserId");
            RenameColumn(table: "dbo.AddressWithIds", name: "ApplicationUser_Id", newName: "UserId");
            DropColumn("dbo.AddressWithIds", "VerificationRequestedByUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressWithIds", "VerificationRequestedByUserId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.AddressWithIds", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.AddressWithIds", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AddressWithIds", "ApplicationUser_Id");
            CreateIndex("dbo.AddressWithIds", "VerificationRequestedByUserId");
            AddForeignKey("dbo.AddressWithIds", "VerificationRequestedByUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
