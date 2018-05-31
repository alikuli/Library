namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _31052018 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UploadedFiles", name: "Product_Id", newName: "ProductId");
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_Product_Id", newName: "IX_ProductId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameColumn(table: "dbo.UploadedFiles", name: "ProductId", newName: "Product_Id");
        }
    }
}
