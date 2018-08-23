namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07082018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikeUnlikes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MenuPath1Id = c.String(maxLength: 128),
                        MenuPath2Id = c.String(maxLength: 128),
                        MenuPath3Id = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        ProductChildId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        IsLike = c.Boolean(nullable: false),
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
                .Index(t => t.MenuPath1Id)
                .Index(t => t.MenuPath2Id)
                .Index(t => t.MenuPath3Id)
                .Index(t => t.ProductId)
                .Index(t => t.ProductChildId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeUnlikes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeUnlikes", "ProductChildId", "dbo.ProductChilds");
            DropForeignKey("dbo.LikeUnlikes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.LikeUnlikes", "MenuPath3Id", "dbo.MenuPath3");
            DropForeignKey("dbo.LikeUnlikes", "MenuPath2Id", "dbo.MenuPath2");
            DropForeignKey("dbo.LikeUnlikes", "MenuPath1Id", "dbo.MenuPath1");
            DropIndex("dbo.LikeUnlikes", new[] { "UserId" });
            DropIndex("dbo.LikeUnlikes", new[] { "ProductChildId" });
            DropIndex("dbo.LikeUnlikes", new[] { "ProductId" });
            DropIndex("dbo.LikeUnlikes", new[] { "MenuPath3Id" });
            DropIndex("dbo.LikeUnlikes", new[] { "MenuPath2Id" });
            DropIndex("dbo.LikeUnlikes", new[] { "MenuPath1Id" });
            DropTable("dbo.LikeUnlikes");
        }
    }
}
