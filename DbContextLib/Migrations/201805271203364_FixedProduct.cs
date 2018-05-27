namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedProduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Qty_LastQtyError");
            DropColumn("dbo.Products", "Qty_LastUomStockName");
            DropColumn("dbo.Products", "Qty_LastQtyErrorDate");
            DropColumn("dbo.Products", "Qty_PreviousToLastQtyError");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStockName");
            DropColumn("dbo.Products", "Qty_PreviousToLastQtyErrorDate");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastQtyError");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStockName");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastQtyErrorDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Qty_B4PreviousToLastQtyErrorDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStockName", c => c.String());
            AddColumn("dbo.Products", "Qty_B4PreviousToLastQtyError", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Qty_PreviousToLastQtyErrorDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStockName", c => c.String());
            AddColumn("dbo.Products", "Qty_PreviousToLastQtyError", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Qty_LastQtyErrorDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "Qty_LastUomStockName", c => c.String());
            AddColumn("dbo.Products", "Qty_LastQtyError", c => c.Double(nullable: false));
        }
    }
}
