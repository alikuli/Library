namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020520181 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProductChilds", name: "OwnerId", newName: "UserId");
            RenameIndex(table: "dbo.ProductChilds", name: "IX_OwnerId", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductChilds", name: "IX_UserId", newName: "IX_OwnerId");
            RenameColumn(table: "dbo.ProductChilds", name: "UserId", newName: "OwnerId");
        }
    }
}
