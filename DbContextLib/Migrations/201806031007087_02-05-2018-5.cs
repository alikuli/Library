namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020520185 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductIdentifiers", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductIdentifiers", new[] { "ProductId" });
            AlterColumn("dbo.ProductIdentifiers", "ProductId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ProductIdentifiers", "ProductId");
            AddForeignKey("dbo.ProductIdentifiers", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductIdentifiers", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductIdentifiers", new[] { "ProductId" });
            AlterColumn("dbo.ProductIdentifiers", "ProductId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProductIdentifiers", "ProductId");
            AddForeignKey("dbo.ProductIdentifiers", "ProductId", "dbo.Products", "Id");
        }
    }
}
