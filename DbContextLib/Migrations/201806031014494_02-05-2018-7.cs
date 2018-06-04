namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020520187 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductChilds", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProductChilds", new[] { "UserId" });
            AlterColumn("dbo.ProductChilds", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ProductChilds", "UserId");
            AddForeignKey("dbo.ProductChilds", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductChilds", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProductChilds", new[] { "UserId" });
            AlterColumn("dbo.ProductChilds", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProductChilds", "UserId");
            AddForeignKey("dbo.ProductChilds", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
