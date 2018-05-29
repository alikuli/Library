namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _280520181 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "UomShipWeightId", newName: "UomWeightActualId");
            RenameColumn(table: "dbo.Products", name: "UomUomWeight_Id", newName: "UomWeightListedId");
            RenameIndex(table: "dbo.Products", name: "IX_UomUomWeight_Id", newName: "IX_UomWeightListedId");
            RenameIndex(table: "dbo.Products", name: "IX_UomShipWeightId", newName: "IX_UomWeightActualId");
            AddColumn("dbo.Products", "WeightListed", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "WeightActual", c => c.Double(nullable: false));
            DropColumn("dbo.Products", "UomWeightId");
            DropColumn("dbo.Products", "Weight");
            DropColumn("dbo.Products", "ShipWeight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ShipWeight", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Weight", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "UomWeightId", c => c.String());
            DropColumn("dbo.Products", "WeightActual");
            DropColumn("dbo.Products", "WeightListed");
            RenameIndex(table: "dbo.Products", name: "IX_UomWeightActualId", newName: "IX_UomShipWeightId");
            RenameIndex(table: "dbo.Products", name: "IX_UomWeightListedId", newName: "IX_UomUomWeight_Id");
            RenameColumn(table: "dbo.Products", name: "UomWeightListedId", newName: "UomUomWeight_Id");
            RenameColumn(table: "dbo.Products", name: "UomWeightActualId", newName: "UomShipWeightId");
        }
    }
}
