namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _241020182 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AddressVerificationHdrs", "AddressVerificationTrxId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressVerificationHdrs", "AddressVerificationTrxId", c => c.String());
        }
    }
}
