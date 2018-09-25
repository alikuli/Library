namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25092018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuPath2Feature",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MenuPath2Id = c.String(maxLength: 128),
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
                .ForeignKey("dbo.MenuPath2", t => t.MenuPath2Id)
                .Index(t => t.MenuPath2Id);
            
            CreateTable(
                "dbo.MenuPath3Feature",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MenuPath3Id = c.String(maxLength: 128),
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
                .ForeignKey("dbo.MenuPath3", t => t.MenuPath3Id)
                .Index(t => t.MenuPath3Id);
            
            AddColumn("dbo.Features", "MenuPath2_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Features", "MenuPath3_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Features", "MenuPath2_Id");
            CreateIndex("dbo.Features", "MenuPath3_Id");
            AddForeignKey("dbo.Features", "MenuPath2_Id", "dbo.MenuPath2", "Id");
            AddForeignKey("dbo.Features", "MenuPath3_Id", "dbo.MenuPath3", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuPath3Feature", "MenuPath3Id", "dbo.MenuPath3");
            DropForeignKey("dbo.Features", "MenuPath3_Id", "dbo.MenuPath3");
            DropForeignKey("dbo.MenuPath2Feature", "MenuPath2Id", "dbo.MenuPath2");
            DropForeignKey("dbo.Features", "MenuPath2_Id", "dbo.MenuPath2");
            DropIndex("dbo.MenuPath3Feature", new[] { "MenuPath3Id" });
            DropIndex("dbo.MenuPath2Feature", new[] { "MenuPath2Id" });
            DropIndex("dbo.Features", new[] { "MenuPath3_Id" });
            DropIndex("dbo.Features", new[] { "MenuPath2_Id" });
            DropColumn("dbo.Features", "MenuPath3_Id");
            DropColumn("dbo.Features", "MenuPath2_Id");
            DropTable("dbo.MenuPath3Feature");
            DropTable("dbo.MenuPath2Feature");
        }
    }
}
