namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _151020181 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressWithIds", "Verification_VerificationNumber", c => c.Long(nullable: false));
            DropColumn("dbo.AddressWithIds", "Verification_ProccessExoirationDate_DateStart");
            DropColumn("dbo.AddressWithIds", "Verification_ProccessExoirationDate_Date");
            DropColumn("dbo.AddressWithIds", "Verification_ProccessExoirationDate_By");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressWithIds", "Verification_ProccessExoirationDate_By", c => c.String(maxLength: 50));
            AddColumn("dbo.AddressWithIds", "Verification_ProccessExoirationDate_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddressWithIds", "Verification_ProccessExoirationDate_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.AddressWithIds", "Verification_VerificationNumber");
        }
    }
}
