namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _250820181 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Features", "FeaturesTypeEnum", c => c.Int(nullable: false));
            DropColumn("dbo.Features", "TypeEnum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Features", "TypeEnum", c => c.Int(nullable: false));
            DropColumn("dbo.Features", "FeaturesTypeEnum");
        }
    }
}
