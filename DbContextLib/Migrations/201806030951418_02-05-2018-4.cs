namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020520184 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductMenuPathMains", newName: "MenuPathMainProducts");
            DropPrimaryKey("dbo.MenuPathMainProducts");
            AddPrimaryKey("dbo.MenuPathMainProducts", new[] { "MenuPathMain_Id", "Product_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MenuPathMainProducts");
            AddPrimaryKey("dbo.MenuPathMainProducts", new[] { "Product_Id", "MenuPathMain_Id" });
            RenameTable(name: "dbo.MenuPathMainProducts", newName: "ProductMenuPathMains");
        }
    }
}
