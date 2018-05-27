namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedNameOfProdCat1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductCategory1", newName: "MenuPath1");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MenuPath1", newName: "ProductCategory1");
        }
    }
}
