namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _191020181 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressWithIds", "Verification_VerificaionStatusEnum", c => c.Int(nullable: false));
            DropColumn("dbo.AddressWithIds", "Verification_Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressWithIds", "Verification_Status", c => c.Int(nullable: false));
            DropColumn("dbo.AddressWithIds", "Verification_VerificaionStatusEnum");
        }
    }
}
