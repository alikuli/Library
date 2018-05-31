namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _310520188 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuPathMains", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.MenuPathMains", "Product_Id1", "dbo.Products");
            DropForeignKey("dbo.Products", "MenuPathMain_Id", "dbo.MenuPathMains");
            DropIndex("dbo.MenuPathMains", new[] { "Product_Id" });
            DropIndex("dbo.MenuPathMains", new[] { "Product_Id1" });
            DropIndex("dbo.Products", new[] { "MenuPathMain_Id" });
            CreateTable(
                "dbo.ProductMenuPathMains",
                c => new
                    {
                        Product_Id = c.String(nullable: false, maxLength: 128),
                        MenuPathMain_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.MenuPathMain_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.MenuPathMains", t => t.MenuPathMain_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.MenuPathMain_Id);
            
            DropColumn("dbo.MenuPathMains", "Product_Id");
            DropColumn("dbo.MenuPathMains", "Product_Id1");
            DropColumn("dbo.Products", "MenuPathMain_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "MenuPathMain_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.MenuPathMains", "Product_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.MenuPathMains", "Product_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ProductMenuPathMains", "MenuPathMain_Id", "dbo.MenuPathMains");
            DropForeignKey("dbo.ProductMenuPathMains", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductMenuPathMains", new[] { "MenuPathMain_Id" });
            DropIndex("dbo.ProductMenuPathMains", new[] { "Product_Id" });
            DropTable("dbo.ProductMenuPathMains");
            CreateIndex("dbo.Products", "MenuPathMain_Id");
            CreateIndex("dbo.MenuPathMains", "Product_Id1");
            CreateIndex("dbo.MenuPathMains", "Product_Id");
            AddForeignKey("dbo.Products", "MenuPathMain_Id", "dbo.MenuPathMains", "Id");
            AddForeignKey("dbo.MenuPathMains", "Product_Id1", "dbo.Products", "Id");
            AddForeignKey("dbo.MenuPathMains", "Product_Id", "dbo.Products", "Id");
        }
    }
}
