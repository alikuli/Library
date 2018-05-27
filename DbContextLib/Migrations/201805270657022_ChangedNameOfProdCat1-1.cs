namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedNameOfProdCat11 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UploadedFiles", name: "ProductCategory1Id", newName: "MenuPath1Id");
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_ProductCategory1Id", newName: "IX_MenuPath1Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_MenuPath1Id", newName: "IX_ProductCategory1Id");
            RenameColumn(table: "dbo.UploadedFiles", name: "MenuPath1Id", newName: "ProductCategory1Id");
        }
    }
}
