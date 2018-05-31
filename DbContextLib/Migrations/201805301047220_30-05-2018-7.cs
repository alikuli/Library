namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _300520187 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "UomQty_Id", "dbo.UomQties");
            DropIndex("dbo.Products", new[] { "UomPurchaseId" });
            DropIndex("dbo.Products", new[] { "UomSaleId" });
            DropIndex("dbo.Products", new[] { "UomQty_Id" });
            AlterColumn("dbo.Products", "UomPurchaseId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "UomSaleId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Products", "UomPurchaseId");
            CreateIndex("dbo.Products", "UomSaleId");
            DropColumn("dbo.Products", "UomQty_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "UomQty_Id", c => c.String(maxLength: 128));
            DropIndex("dbo.Products", new[] { "UomSaleId" });
            DropIndex("dbo.Products", new[] { "UomPurchaseId" });
            AlterColumn("dbo.Products", "UomSaleId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "UomPurchaseId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "UomQty_Id");
            CreateIndex("dbo.Products", "UomSaleId");
            CreateIndex("dbo.Products", "UomPurchaseId");
            AddForeignKey("dbo.Products", "UomQty_Id", "dbo.UomQties", "Id");
        }
    }
}
