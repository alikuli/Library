namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _220920182 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeatureMenuPath1",
                c => new
                    {
                        Feature_Id = c.String(nullable: false, maxLength: 128),
                        MenuPath1_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Feature_Id, t.MenuPath1_Id })
                .ForeignKey("dbo.Features", t => t.Feature_Id, cascadeDelete: true)
                .ForeignKey("dbo.MenuPath1", t => t.MenuPath1_Id, cascadeDelete: true)
                .Index(t => t.Feature_Id)
                .Index(t => t.MenuPath1_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeatureMenuPath1", "MenuPath1_Id", "dbo.MenuPath1");
            DropForeignKey("dbo.FeatureMenuPath1", "Feature_Id", "dbo.Features");
            DropIndex("dbo.FeatureMenuPath1", new[] { "MenuPath1_Id" });
            DropIndex("dbo.FeatureMenuPath1", new[] { "Feature_Id" });
            DropTable("dbo.FeatureMenuPath1");
        }
    }
}
