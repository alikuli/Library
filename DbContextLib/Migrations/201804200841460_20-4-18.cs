namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20418 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UomQties",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UnitsToMakeOneOfBase = c.Double(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Products", "UomPurchaseId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "UomStockID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "UomPurchaseId");
            CreateIndex("dbo.Products", "UomStockID");
            AddForeignKey("dbo.Products", "UomPurchaseId", "dbo.UomQties", "Id");
            AddForeignKey("dbo.Products", "UomStockID", "dbo.UomQties", "Id");
            DropColumn("dbo.Products", "Qty_LastUomStock_UnitsToMakeOneOfBase");
            DropColumn("dbo.Products", "Qty_LastUomStock_Id");
            DropColumn("dbo.Products", "Qty_LastUomStock_Comment");
            DropColumn("dbo.Products", "Qty_LastUomStock_DetailInfoToDisplayOnWebsite");
            DropColumn("dbo.Products", "Qty_LastUomStock_Name");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_IsEditLocked");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_IsActive");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_IsDeleted");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_Created_DateStart");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_Created_Date");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_Created_By");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_Deleted_DateStart");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_Deleted_Date");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_Deleted_By");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_Modified_DateStart");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_Modified_Date");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_Modified_By");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_UnDeleted_DateStart");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_UnDeleted_Date");
            DropColumn("dbo.Products", "Qty_LastUomStock_MetaData_UnDeleted_By");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_UnitsToMakeOneOfBase");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_Id");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_Comment");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_DetailInfoToDisplayOnWebsite");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_Name");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_IsEditLocked");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_IsActive");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_IsDeleted");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Created_DateStart");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Created_Date");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Created_By");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Deleted_DateStart");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Deleted_Date");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Deleted_By");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Modified_DateStart");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Modified_Date");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Modified_By");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_UnDeleted_DateStart");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_UnDeleted_Date");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_UnDeleted_By");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_UnitsToMakeOneOfBase");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_Id");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_Comment");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_DetailInfoToDisplayOnWebsite");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_Name");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_IsEditLocked");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_IsActive");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_IsDeleted");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Created_DateStart");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Created_Date");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Created_By");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Deleted_DateStart");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Deleted_Date");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Deleted_By");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Modified_DateStart");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Modified_Date");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Modified_By");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_UnDeleted_DateStart");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_UnDeleted_Date");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_UnDeleted_By");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_UnDeleted_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_UnDeleted_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_UnDeleted_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Modified_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Modified_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Modified_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Deleted_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Deleted_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Deleted_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Created_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Created_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_Created_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_MetaData_IsEditLocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_Name", c => c.String(maxLength: 2000));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_DetailInfoToDisplayOnWebsite", c => c.String());
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_Comment", c => c.String(maxLength: 1000));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_Id", c => c.String());
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStock_UnitsToMakeOneOfBase", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_UnDeleted_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_UnDeleted_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_UnDeleted_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Modified_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Modified_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Modified_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Deleted_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Deleted_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Deleted_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Created_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Created_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_Created_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_MetaData_IsEditLocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_Name", c => c.String(maxLength: 2000));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_DetailInfoToDisplayOnWebsite", c => c.String());
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_Comment", c => c.String(maxLength: 1000));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_Id", c => c.String());
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStock_UnitsToMakeOneOfBase", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_UnDeleted_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_UnDeleted_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_UnDeleted_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_Modified_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_Modified_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_Modified_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_Deleted_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_Deleted_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_Deleted_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_Created_By", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_Created_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_Created_DateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Qty_LastUomStock_MetaData_IsEditLocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Qty_LastUomStock_Name", c => c.String(maxLength: 2000));
            AddColumn("dbo.Products", "Qty_LastUomStock_DetailInfoToDisplayOnWebsite", c => c.String());
            AddColumn("dbo.Products", "Qty_LastUomStock_Comment", c => c.String(maxLength: 1000));
            AddColumn("dbo.Products", "Qty_LastUomStock_Id", c => c.String());
            AddColumn("dbo.Products", "Qty_LastUomStock_UnitsToMakeOneOfBase", c => c.Double(nullable: false));
            DropForeignKey("dbo.Products", "UomStockID", "dbo.UomQties");
            DropForeignKey("dbo.Products", "UomPurchaseId", "dbo.UomQties");
            DropIndex("dbo.Products", new[] { "UomStockID" });
            DropIndex("dbo.Products", new[] { "UomPurchaseId" });
            AlterColumn("dbo.Products", "UomStockID", c => c.String());
            AlterColumn("dbo.Products", "UomPurchaseId", c => c.String());
            DropTable("dbo.UomQties");
        }
    }
}
