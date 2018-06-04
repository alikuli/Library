namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0205201810 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductIdentifiers", new[] { "ProductId" });
            AlterColumn("dbo.ProductIdentifiers", "ProductId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProductIdentifiers", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductIdentifiers", new[] { "ProductId" });
            AlterColumn("dbo.ProductIdentifiers", "ProductId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ProductIdentifiers", "ProductId");
        }
    }
}
