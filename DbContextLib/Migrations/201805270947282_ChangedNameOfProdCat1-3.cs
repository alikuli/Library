namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedNameOfProdCat13 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductCategory3", newName: "MenuPath3");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MenuPath3", newName: "ProductCategory3");
        }
    }
}
