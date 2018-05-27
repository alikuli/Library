namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _250520181 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "UomPackageLengthId", newName: "UomDimensionsId");
            RenameIndex(table: "dbo.Products", name: "IX_UomPackageLengthId", newName: "IX_UomDimensionsId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_UomDimensionsId", newName: "IX_UomPackageLengthId");
            RenameColumn(table: "dbo.Products", name: "UomDimensionsId", newName: "UomPackageLengthId");
        }
    }
}
