namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedNameOfProdCat14 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductCategoryMains", newName: "MenuPathMains");
            RenameColumn(table: "dbo.MenuPathMains", name: "ProductCat1Id", newName: "MenuPath1Id");
            RenameColumn(table: "dbo.MenuPathMains", name: "ProductCat2Id", newName: "MenuPath2Id");
            RenameColumn(table: "dbo.MenuPathMains", name: "ProductCat3Id", newName: "MenuPath3Id");
            RenameColumn(table: "dbo.UploadedFiles", name: "ProductCategoryMain_Id", newName: "MenuPathMain_Id");
            RenameColumn(table: "dbo.Products", name: "ProductCategoryMain_Id", newName: "MenuPathMain_Id");
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_ProductCategoryMain_Id", newName: "IX_MenuPathMain_Id");
            RenameIndex(table: "dbo.MenuPathMains", name: "IX_ProductCat1Id", newName: "IX_MenuPath1Id");
            RenameIndex(table: "dbo.MenuPathMains", name: "IX_ProductCat2Id", newName: "IX_MenuPath2Id");
            RenameIndex(table: "dbo.MenuPathMains", name: "IX_ProductCat3Id", newName: "IX_MenuPath3Id");
            RenameIndex(table: "dbo.Products", name: "IX_ProductCategoryMain_Id", newName: "IX_MenuPathMain_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_MenuPathMain_Id", newName: "IX_ProductCategoryMain_Id");
            RenameIndex(table: "dbo.MenuPathMains", name: "IX_MenuPath3Id", newName: "IX_ProductCat3Id");
            RenameIndex(table: "dbo.MenuPathMains", name: "IX_MenuPath2Id", newName: "IX_ProductCat2Id");
            RenameIndex(table: "dbo.MenuPathMains", name: "IX_MenuPath1Id", newName: "IX_ProductCat1Id");
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_MenuPathMain_Id", newName: "IX_ProductCategoryMain_Id");
            RenameColumn(table: "dbo.Products", name: "MenuPathMain_Id", newName: "ProductCategoryMain_Id");
            RenameColumn(table: "dbo.UploadedFiles", name: "MenuPathMain_Id", newName: "ProductCategoryMain_Id");
            RenameColumn(table: "dbo.MenuPathMains", name: "MenuPath3Id", newName: "ProductCat3Id");
            RenameColumn(table: "dbo.MenuPathMains", name: "MenuPath2Id", newName: "ProductCat2Id");
            RenameColumn(table: "dbo.MenuPathMains", name: "MenuPath1Id", newName: "ProductCat1Id");
            RenameTable(name: "dbo.MenuPathMains", newName: "ProductCategoryMains");
        }
    }
}
