namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _031120182 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AddressVerificationTrxes", "VerificationNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressVerificationTrxes", "VerificationNumber", c => c.Long(nullable: false));
        }
    }
}
