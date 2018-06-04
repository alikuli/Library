namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020520186 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductChilds", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductChilds", new[] { "ProductId" });
            AlterColumn("dbo.ProductChilds", "ProductId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ProductChilds", "ProductId");
            AddForeignKey("dbo.ProductChilds", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductChilds", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductChilds", new[] { "ProductId" });
            AlterColumn("dbo.ProductChilds", "ProductId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProductChilds", "ProductId");
            AddForeignKey("dbo.ProductChilds", "ProductId", "dbo.Products", "Id");
        }
    }
}
