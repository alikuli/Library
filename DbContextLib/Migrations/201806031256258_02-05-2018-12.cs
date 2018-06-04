namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0205201812 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductChilds", new[] { "ProductId" });
            AlterColumn("dbo.ProductChilds", "ProductId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProductChilds", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductChilds", new[] { "ProductId" });
            AlterColumn("dbo.ProductChilds", "ProductId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ProductChilds", "ProductId");
        }
    }
}
