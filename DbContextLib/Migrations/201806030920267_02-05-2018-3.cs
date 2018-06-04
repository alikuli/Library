namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020520183 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UploadedFiles", name: "ProductChild_Id", newName: "ProductChildId");
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_ProductChild_Id", newName: "IX_ProductChildId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UploadedFiles", name: "IX_ProductChildId", newName: "IX_ProductChild_Id");
            RenameColumn(table: "dbo.UploadedFiles", name: "ProductChildId", newName: "ProductChild_Id");
        }
    }
}
