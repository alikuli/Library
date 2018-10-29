namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24102018 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId", "dbo.AspNetUsers");
            DropIndex("dbo.AddressVerificationTrxes", new[] { "VerificationOrderedByUserId" });
            AddColumn("dbo.AddressVerificationTrxes", "MailLocalOrForiegnEnum", c => c.Int(nullable: false));
            DropColumn("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId", c => c.String(maxLength: 128));
            DropColumn("dbo.AddressVerificationTrxes", "MailLocalOrForiegnEnum");
            CreateIndex("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId");
            AddForeignKey("dbo.AddressVerificationTrxes", "VerificationOrderedByUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
