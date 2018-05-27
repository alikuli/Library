namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedNameOfProdCat12 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductCategory2", newName: "MenuPath2");
            RenameColumn(table: "dbo.UploadedFiles", name: "ProductCategory2Id", newName: "MenuPath2Id");
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_ProductCategory2Id", newName: "IX_MenuPath2Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_MenuPath2Id", newName: "IX_ProductCategory2Id");
            RenameColumn(table: "dbo.UploadedFiles", name: "MenuPath2Id", newName: "ProductCategory2Id");
            RenameTable(name: "dbo.MenuPath2", newName: "ProductCategory2");
        }
    }
}
