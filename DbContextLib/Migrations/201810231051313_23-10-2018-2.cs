namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _231020182 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AddressWithIds", "UserId", "dbo.AspNetUsers");
            AddColumn("dbo.AddressWithIds", "VerificationRequestedByUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.AddressWithIds", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AddressWithIds", "VerificationRequestedByUserId");
            CreateIndex("dbo.AddressWithIds", "ApplicationUser_Id");
            AddForeignKey("dbo.AddressWithIds", "VerificationRequestedByUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AddressWithIds", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddressWithIds", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AddressWithIds", "VerificationRequestedByUserId", "dbo.AspNetUsers");
            DropIndex("dbo.AddressWithIds", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AddressWithIds", new[] { "VerificationRequestedByUserId" });
            DropColumn("dbo.AddressWithIds", "ApplicationUser_Id");
            DropColumn("dbo.AddressWithIds", "VerificationRequestedByUserId");
            AddForeignKey("dbo.AddressWithIds", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
