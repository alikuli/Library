namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _220920181 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Features", "MenuPath2Id", "dbo.MenuPath2");
            DropForeignKey("dbo.Features", "MenuPath3Id", "dbo.MenuPath3");
            DropForeignKey("dbo.Features", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Features", "ProductChildId", "dbo.ProductChilds");
            DropForeignKey("dbo.Features", "MenuPath1Id", "dbo.MenuPath1");
            DropIndex("dbo.Features", new[] { "MenuPath1Id" });
            DropIndex("dbo.Features", new[] { "MenuPath2Id" });
            DropIndex("dbo.Features", new[] { "MenuPath3Id" });
            DropIndex("dbo.Features", new[] { "ProductId" });
            DropIndex("dbo.Features", new[] { "ProductChildId" });
            DropColumn("dbo.Features", "MenuPath1Id");
            DropColumn("dbo.Features", "MenuPath2Id");
            DropColumn("dbo.Features", "MenuPath3Id");
            DropColumn("dbo.Features", "ProductId");
            DropColumn("dbo.Features", "ProductChildId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Features", "ProductChildId", c => c.String(maxLength: 128));
            AddColumn("dbo.Features", "ProductId", c => c.String(maxLength: 128));
            AddColumn("dbo.Features", "MenuPath3Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Features", "MenuPath2Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Features", "MenuPath1Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Features", "ProductChildId");
            CreateIndex("dbo.Features", "ProductId");
            CreateIndex("dbo.Features", "MenuPath3Id");
            CreateIndex("dbo.Features", "MenuPath2Id");
            CreateIndex("dbo.Features", "MenuPath1Id");
            AddForeignKey("dbo.Features", "MenuPath1Id", "dbo.MenuPath1", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Features", "ProductChildId", "dbo.ProductChilds", "Id");
            AddForeignKey("dbo.Features", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Features", "MenuPath3Id", "dbo.MenuPath3", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Features", "MenuPath2Id", "dbo.MenuPath2", "Id", cascadeDelete: true);
        }
    }
}
