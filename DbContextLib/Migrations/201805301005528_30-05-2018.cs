namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30052018 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "UomDimensionsId", "dbo.UomLengths");
            DropForeignKey("dbo.Products", "UomPurchaseId", "dbo.UomQties");
            DropIndex("dbo.Products", new[] { "UomDimensionsId" });
            AddColumn("dbo.Products", "UomSaleId", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "UomQty_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "UomDimensionsId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Products", "UomSaleId");
            CreateIndex("dbo.Products", "UomDimensionsId");
            CreateIndex("dbo.Products", "UomQty_Id");
            AddForeignKey("dbo.Products", "UomSaleId", "dbo.UomQties", "Id");
            AddForeignKey("dbo.Products", "UomDimensionsId", "dbo.UomLengths", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "UomQty_Id", "dbo.UomQties", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UomQty_Id", "dbo.UomQties");
            DropForeignKey("dbo.Products", "UomDimensionsId", "dbo.UomLengths");
            DropForeignKey("dbo.Products", "UomSaleId", "dbo.UomQties");
            DropIndex("dbo.Products", new[] { "UomQty_Id" });
            DropIndex("dbo.Products", new[] { "UomDimensionsId" });
            DropIndex("dbo.Products", new[] { "UomSaleId" });
            AlterColumn("dbo.Products", "UomDimensionsId", c => c.String(maxLength: 128));
            DropColumn("dbo.Products", "UomQty_Id");
            DropColumn("dbo.Products", "UomSaleId");
            CreateIndex("dbo.Products", "UomDimensionsId");
            AddForeignKey("dbo.Products", "UomPurchaseId", "dbo.UomQties", "Id");
            AddForeignKey("dbo.Products", "UomDimensionsId", "dbo.UomLengths", "Id");
        }
    }
}
