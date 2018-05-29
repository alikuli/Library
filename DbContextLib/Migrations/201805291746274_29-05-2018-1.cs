namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _290520181 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UomWeight_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "UomWeight_Id");
            AddForeignKey("dbo.Products", "UomWeight_Id", "dbo.UomWeights", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UomWeight_Id", "dbo.UomWeights");
            DropIndex("dbo.Products", new[] { "UomWeight_Id" });
            DropColumn("dbo.Products", "UomWeight_Id");
        }
    }
}
