namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _241020184 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressVerificationTrxes", "VerificationNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AddressVerificationTrxes", "VerificationNumber");
        }
    }
}
