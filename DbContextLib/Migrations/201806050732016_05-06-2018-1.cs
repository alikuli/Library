namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _050620181 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductChilds", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "UomPurchaseId" });
            DropIndex("dbo.Products", new[] { "UomSaleId" });
            DropIndex("dbo.Products", new[] { "UomVolumeId" });
            DropIndex("dbo.Products", new[] { "UomWeightListedId" });
            DropIndex("dbo.Products", new[] { "UomWeightActualId" });
            DropIndex("dbo.Products", new[] { "UomDimensionsId" });
            AlterColumn("dbo.ProductChilds", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "UomPurchaseId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "UomSaleId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "UomVolumeId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "UomWeightListedId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "UomWeightActualId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "UomDimensionsId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProductChilds", "UserId");
            CreateIndex("dbo.Products", "UomPurchaseId");
            CreateIndex("dbo.Products", "UomSaleId");
            CreateIndex("dbo.Products", "UomVolumeId");
            CreateIndex("dbo.Products", "UomWeightListedId");
            CreateIndex("dbo.Products", "UomWeightActualId");
            CreateIndex("dbo.Products", "UomDimensionsId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "UomDimensionsId" });
            DropIndex("dbo.Products", new[] { "UomWeightActualId" });
            DropIndex("dbo.Products", new[] { "UomWeightListedId" });
            DropIndex("dbo.Products", new[] { "UomVolumeId" });
            DropIndex("dbo.Products", new[] { "UomSaleId" });
            DropIndex("dbo.Products", new[] { "UomPurchaseId" });
            DropIndex("dbo.ProductChilds", new[] { "UserId" });
            AlterColumn("dbo.Products", "UomDimensionsId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "UomWeightActualId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "UomWeightListedId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "UomVolumeId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "UomSaleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "UomPurchaseId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ProductChilds", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Products", "UomDimensionsId");
            CreateIndex("dbo.Products", "UomWeightActualId");
            CreateIndex("dbo.Products", "UomWeightListedId");
            CreateIndex("dbo.Products", "UomVolumeId");
            CreateIndex("dbo.Products", "UomSaleId");
            CreateIndex("dbo.Products", "UomPurchaseId");
            CreateIndex("dbo.ProductChilds", "UserId");
        }
    }
}
