namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _311020181 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AddressWithIds", "Verification_VerificationNumber");
            DropColumn("dbo.AddressWithIds", "Verification_VerificaionStatusEnum");
            DropColumn("dbo.AddressWithIds", "Verification_RequestDate_DateStart");
            DropColumn("dbo.AddressWithIds", "Verification_RequestDate_Date");
            DropColumn("dbo.AddressWithIds", "Verification_RequestDate_By");
            DropColumn("dbo.AddressWithIds", "Verification_AcceptDate_DateStart");
            DropColumn("dbo.AddressWithIds", "Verification_AcceptDate_Date");
            DropColumn("dbo.AddressWithIds", "Verification_AcceptDate_By");
            DropColumn("dbo.AddressWithIds", "Verification_PrintedDate_DateStart");
            DropColumn("dbo.AddressWithIds", "Verification_PrintedDate_Date");
            DropColumn("dbo.AddressWithIds", "Verification_PrintedDate_By");
            DropColumn("dbo.AddressWithIds", "Verification_FailedDate_DateStart");
            DropColumn("dbo.AddressWithIds", "Verification_FailedDate_Date");
            DropColumn("dbo.AddressWithIds", "Verification_FailedDate_By");
            DropColumn("dbo.AddressWithIds", "Verification_VerifiedDate_DateStart");
            DropColumn("dbo.AddressWithIds", "Verification_VerifiedDate_Date");
            DropColumn("dbo.AddressWithIds", "Verification_VerifiedDate_By");
            DropColumn("dbo.AddressWithIds", "Verification_MailedDate_DateStart");
            DropColumn("dbo.AddressWithIds", "Verification_MailedDate_Date");
            DropColumn("dbo.AddressWithIds", "Verification_MailedDate_By");
            DropColumn("dbo.AddressWithIds", "Verification_ProccessExpirationDate_DateStart");
            DropColumn("dbo.AddressWithIds", "Verification_ProccessExpirationDate_Date");
            DropColumn("dbo.AddressWithIds", "Verification_ProccessExpirationDate_By");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressWithIds", "Verification_ProccessExpirationDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressWithIds", "Verification_ProccessExpirationDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_ProccessExpirationDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_MailedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressWithIds", "Verification_MailedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_MailedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_VerifiedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressWithIds", "Verification_VerifiedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_VerifiedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_FailedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressWithIds", "Verification_FailedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_FailedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_PrintedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressWithIds", "Verification_PrintedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_PrintedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_AcceptDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressWithIds", "Verification_AcceptDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_AcceptDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_RequestDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressWithIds", "Verification_RequestDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_RequestDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_VerificaionStatusEnum", c => c.Int(nullable: false));
            AddColumn("dbo.AddressWithIds", "Verification_VerificationNumber", c => c.Long(nullable: false));
        }
    }
}
