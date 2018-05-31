namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _300520184 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UomWeightActualId", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "WeightActual", c => c.Double(nullable: false));
            CreateIndex("dbo.Products", "UomWeightActualId");
            AddForeignKey("dbo.Products", "UomWeightActualId", "dbo.UomWeights", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UomWeightActualId", "dbo.UomWeights");
            DropIndex("dbo.Products", new[] { "UomWeightActualId" });
            DropColumn("dbo.Products", "WeightActual");
            DropColumn("dbo.Products", "UomWeightActualId");
        }
    }
}
