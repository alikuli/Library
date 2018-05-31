namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _300520185 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Products", new[] { "UomWeightActualId" });
            AlterColumn("dbo.Products", "UomWeightActualId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Products", "UomWeightActualId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "UomWeightActualId" });
            AlterColumn("dbo.Products", "UomWeightActualId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "UomWeightActualId");
        }
    }
}
