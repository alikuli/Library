namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23072018 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MenuPathMainProducts", newName: "ProductMenuPathMains");
            DropPrimaryKey("dbo.ProductMenuPathMains");
            CreateTable(
                "dbo.GlobalComments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        MenuPath1Id = c.String(maxLength: 128),
                        MenuPath2Id = c.String(maxLength: 128),
                        MenuPath3Id = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        ProductChildId = c.String(maxLength: 128),
                        Comment = c.String(maxLength: 1000),
                        DetailInfoToDisplayOnWebsite = c.String(),
                        Name = c.String(maxLength: 2000),
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
                .ForeignKey("dbo.MenuPath1", t => t.MenuPath1Id)
                .ForeignKey("dbo.MenuPath2", t => t.MenuPath2Id)
                .ForeignKey("dbo.MenuPath3", t => t.MenuPath3Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.ProductChilds", t => t.ProductChildId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MenuPath1Id)
                .Index(t => t.MenuPath2Id)
                .Index(t => t.MenuPath3Id)
                .Index(t => t.ProductId)
                .Index(t => t.ProductChildId);
            
            AddPrimaryKey("dbo.ProductMenuPathMains", new[] { "Product_Id", "MenuPathMain_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlobalComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GlobalComments", "ProductChildId", "dbo.ProductChilds");
            DropForeignKey("dbo.GlobalComments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.GlobalComments", "MenuPath3Id", "dbo.MenuPath3");
            DropForeignKey("dbo.GlobalComments", "MenuPath2Id", "dbo.MenuPath2");
            DropForeignKey("dbo.GlobalComments", "MenuPath1Id", "dbo.MenuPath1");
            DropIndex("dbo.GlobalComments", new[] { "ProductChildId" });
            DropIndex("dbo.GlobalComments", new[] { "ProductId" });
            DropIndex("dbo.GlobalComments", new[] { "MenuPath3Id" });
            DropIndex("dbo.GlobalComments", new[] { "MenuPath2Id" });
            DropIndex("dbo.GlobalComments", new[] { "MenuPath1Id" });
            DropIndex("dbo.GlobalComments", new[] { "UserId" });
            DropPrimaryKey("dbo.ProductMenuPathMains");
            DropTable("dbo.GlobalComments");
            AddPrimaryKey("dbo.ProductMenuPathMains", new[] { "MenuPathMain_Id", "Product_Id" });
            RenameTable(name: "dbo.ProductMenuPathMains", newName: "MenuPathMainProducts");
        }
    }
}
