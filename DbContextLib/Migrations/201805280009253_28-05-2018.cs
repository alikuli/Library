namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28052018 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "UomStockID", "dbo.UomQties");
            DropIndex("dbo.Products", new[] { "UomStockID" });
            DropColumn("dbo.Products", "UomStockID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "UomStockID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "UomStockID");
            AddForeignKey("dbo.Products", "UomStockID", "dbo.UomQties", "Id");
        }
    }
}
