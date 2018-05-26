namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25052018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductChilds",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OwnerId = c.String(maxLength: 128),
                        Sell_MSRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sell_MlpPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sell_SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Buy_Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpiryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProductId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OwnerId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductIdentifiers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.UploadedFiles", "ProductChild_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "IsSaleable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Dimensions_Height", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Dimensions_Width", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Dimensions_Length", c => c.Double(nullable: false));
            CreateIndex("dbo.UploadedFiles", "ProductChild_Id");
            AddForeignKey("dbo.UploadedFiles", "ProductChild_Id", "dbo.ProductChilds", "Id");
            DropColumn("dbo.Products", "ExpiryDate");
            DropColumn("dbo.Products", "SerialNo");
            DropColumn("dbo.Products", "Dims_Height");
            DropColumn("dbo.Products", "Dims_Width");
            DropColumn("dbo.Products", "Dims_Length");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Dims_Length", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Dims_Width", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Dims_Height", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "SerialNo", c => c.String());
            AddColumn("dbo.Products", "ExpiryDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropForeignKey("dbo.ProductIdentifiers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductChilds", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductChilds", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "ProductChild_Id", "dbo.ProductChilds");
            DropIndex("dbo.ProductIdentifiers", new[] { "ProductId" });
            DropIndex("dbo.ProductChilds", new[] { "ProductId" });
            DropIndex("dbo.ProductChilds", new[] { "OwnerId" });
            DropIndex("dbo.UploadedFiles", new[] { "ProductChild_Id" });
            DropColumn("dbo.Products", "Dimensions_Length");
            DropColumn("dbo.Products", "Dimensions_Width");
            DropColumn("dbo.Products", "Dimensions_Height");
            DropColumn("dbo.Products", "IsSaleable");
            DropColumn("dbo.UploadedFiles", "ProductChild_Id");
            DropTable("dbo.ProductIdentifiers");
            DropTable("dbo.ProductChilds");
        }
    }
}
