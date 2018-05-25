namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20418a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Qty_LastUomStockName", c => c.String());
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStockName", c => c.String());
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStockName", c => c.String());
            DropColumn("dbo.Products", "Qty_LastUomStockID");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStockID");
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStockID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Qty_B4PreviousToLastUomStockID", c => c.Long(nullable: false));
            AddColumn("dbo.Products", "Qty_PreviousToLastUomStockID", c => c.Long(nullable: false));
            AddColumn("dbo.Products", "Qty_LastUomStockID", c => c.Long(nullable: false));
            DropColumn("dbo.Products", "Qty_B4PreviousToLastUomStockName");
            DropColumn("dbo.Products", "Qty_PreviousToLastUomStockName");
            DropColumn("dbo.Products", "Qty_LastUomStockName");
        }
    }
}
