namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29102018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressVerificationTrxes", "LetterNo", c => c.Long(nullable: false));
            AddColumn("dbo.AddressVerificationHdrs", "BatchNo", c => c.Long(nullable: false));
            AddColumn("dbo.AddressVerificationHdrs", "MailLocalOrForiegnEnum", c => c.Int(nullable: false));
            AddColumn("dbo.AddressVerificationHdrs", "MailServiceEnum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AddressVerificationHdrs", "MailServiceEnum");
            DropColumn("dbo.AddressVerificationHdrs", "MailLocalOrForiegnEnum");
            DropColumn("dbo.AddressVerificationHdrs", "BatchNo");
            DropColumn("dbo.AddressVerificationTrxes", "LetterNo");
        }
    }
}
