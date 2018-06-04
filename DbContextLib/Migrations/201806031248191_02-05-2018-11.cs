namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0205201811 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UploadedFiles", "ProductId", "dbo.Products");
            AddForeignKey("dbo.UploadedFiles", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UploadedFiles", "ProductId", "dbo.Products");
            AddForeignKey("dbo.UploadedFiles", "ProductId", "dbo.Products", "Id");
        }
    }
}
