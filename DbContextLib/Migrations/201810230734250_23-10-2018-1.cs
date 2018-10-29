namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _231020181 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressVerificationTrxes", "DateVerifcationPaymentAccepted", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AddressVerificationTrxes", "DateVerifcationPaymentAccepted");
        }
    }
}
