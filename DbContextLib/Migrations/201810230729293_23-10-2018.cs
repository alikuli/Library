namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23102018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId");
            AddForeignKey("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId", "dbo.AspNetUsers");
            DropIndex("dbo.AddressVerificationTrxes", new[] { "VerificationOrderedByUserId" });
            DropColumn("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId");
        }
    }
}
