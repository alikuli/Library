namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _220920185 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuPath1Feature", "FeatureId", "dbo.Features");
            DropIndex("dbo.MenuPath1Feature", new[] { "FeatureId" });
            DropColumn("dbo.MenuPath1Feature", "FeatureId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MenuPath1Feature", "FeatureId", c => c.String(maxLength: 128));
            CreateIndex("dbo.MenuPath1Feature", "FeatureId");
            AddForeignKey("dbo.MenuPath1Feature", "FeatureId", "dbo.Features", "Id");
        }
    }
}
