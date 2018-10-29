namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _241020181 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AddressVerificationTrxes", "SuccessEnum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressVerificationTrxes", "SuccessEnum", c => c.Int(nullable: false));
        }
    }
}
