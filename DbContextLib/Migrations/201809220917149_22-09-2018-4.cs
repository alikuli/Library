namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _220920184 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FeatureMenuPath1", "Feature_Id", "dbo.Features");
            DropForeignKey("dbo.FeatureMenuPath1", "MenuPath1_Id", "dbo.MenuPath1");
            DropIndex("dbo.FeatureMenuPath1", new[] { "Feature_Id" });
            DropIndex("dbo.FeatureMenuPath1", new[] { "MenuPath1_Id" });
            AddColumn("dbo.MenuPath1", "Feature_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.MenuPath1", "Feature_Id");
            AddForeignKey("dbo.MenuPath1", "Feature_Id", "dbo.Features", "Id");
            DropTable("dbo.FeatureMenuPath1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FeatureMenuPath1",
                c => new
                    {
                        Feature_Id = c.String(nullable: false, maxLength: 128),
                        MenuPath1_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Feature_Id, t.MenuPath1_Id });
            
            DropForeignKey("dbo.MenuPath1", "Feature_Id", "dbo.Features");
            DropIndex("dbo.MenuPath1", new[] { "Feature_Id" });
            DropColumn("dbo.MenuPath1", "Feature_Id");
            CreateIndex("dbo.FeatureMenuPath1", "MenuPath1_Id");
            CreateIndex("dbo.FeatureMenuPath1", "Feature_Id");
            AddForeignKey("dbo.FeatureMenuPath1", "MenuPath1_Id", "dbo.MenuPath1", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FeatureMenuPath1", "Feature_Id", "dbo.Features", "Id", cascadeDelete: true);
        }
    }
}
