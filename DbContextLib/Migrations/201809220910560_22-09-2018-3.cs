namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _220920183 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuPath1Feature",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MenuPath1Id = c.String(maxLength: 128),
                        FeatureId = c.String(maxLength: 128),
                        Comment = c.String(maxLength: 1000),
                        DetailInfoToDisplayOnWebsite = c.String(),
                        Name = c.String(),
                        MetaData_IsEditLocked = c.Boolean(nullable: false),
                        MetaData_IsActive = c.Boolean(nullable: false),
                        MetaData_IsDeleted = c.Boolean(nullable: false),
                        MetaData_Created_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Created_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Created_By = c.String(maxLength: 50),
                        MetaData_Deleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Deleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Deleted_By = c.String(maxLength: 50),
                        MetaData_Modified_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Modified_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Modified_By = c.String(maxLength: 50),
                        MetaData_UnDeleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_UnDeleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_UnDeleted_By = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId)
                .ForeignKey("dbo.MenuPath1", t => t.MenuPath1Id)
                .Index(t => t.MenuPath1Id)
                .Index(t => t.FeatureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuPath1Feature", "MenuPath1Id", "dbo.MenuPath1");
            DropForeignKey("dbo.MenuPath1Feature", "FeatureId", "dbo.Features");
            DropIndex("dbo.MenuPath1Feature", new[] { "FeatureId" });
            DropIndex("dbo.MenuPath1Feature", new[] { "MenuPath1Id" });
            DropTable("dbo.MenuPath1Feature");
        }
    }
}
