namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _301020181 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressVerificationTrxes", "Verification_VerificationNumber", c => c.Long(nullable: false));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_VerificaionStatusEnum", c => c.Int(nullable: false));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_RequestDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_RequestDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_RequestDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_AcceptDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_AcceptDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_AcceptDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_PrintedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_PrintedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_PrintedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_FailedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_FailedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_FailedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_VerifiedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_VerifiedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_VerifiedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_MailedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_MailedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_MailedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_ProccessExpirationDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_ProccessExpirationDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationTrxes", "Verification_ProccessExpirationDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_VerificationNumber", c => c.Long(nullable: false));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_VerificaionStatusEnum", c => c.Int(nullable: false));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_RequestDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_RequestDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_RequestDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_AcceptDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_AcceptDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_AcceptDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_PrintedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_PrintedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_PrintedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_FailedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_FailedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_FailedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_VerifiedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_VerifiedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_VerifiedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_MailedDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_MailedDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_MailedDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_ProccessExpirationDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_ProccessExpirationDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressVerificationHdrs", "Verification_ProccessExpirationDate_By", c => c.String(maxLength: 50));
            DropColumn("dbo.AddressVerificationTrxes", "VerificaionStatusEnum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressVerificationTrxes", "VerificaionStatusEnum", c => c.Int(nullable: false));
            DropColumn("dbo.AddressVerificationHdrs", "Verification_ProccessExpirationDate_By");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_ProccessExpirationDate_Date");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_ProccessExpirationDate_DateStart");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_MailedDate_By");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_MailedDate_Date");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_MailedDate_DateStart");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_VerifiedDate_By");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_VerifiedDate_Date");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_VerifiedDate_DateStart");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_FailedDate_By");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_FailedDate_Date");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_FailedDate_DateStart");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_PrintedDate_By");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_PrintedDate_Date");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_PrintedDate_DateStart");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_AcceptDate_By");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_AcceptDate_Date");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_AcceptDate_DateStart");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_RequestDate_By");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_RequestDate_Date");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_RequestDate_DateStart");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_VerificaionStatusEnum");
            DropColumn("dbo.AddressVerificationHdrs", "Verification_VerificationNumber");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_ProccessExpirationDate_By");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_ProccessExpirationDate_Date");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_ProccessExpirationDate_DateStart");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_MailedDate_By");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_MailedDate_Date");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_MailedDate_DateStart");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_VerifiedDate_By");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_VerifiedDate_Date");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_VerifiedDate_DateStart");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_FailedDate_By");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_FailedDate_Date");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_FailedDate_DateStart");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_PrintedDate_By");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_PrintedDate_Date");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_PrintedDate_DateStart");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_AcceptDate_By");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_AcceptDate_Date");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_AcceptDate_DateStart");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_RequestDate_By");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_RequestDate_Date");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_RequestDate_DateStart");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_VerificaionStatusEnum");
            DropColumn("dbo.AddressVerificationTrxes", "Verification_VerificationNumber");
        }
    }
}
