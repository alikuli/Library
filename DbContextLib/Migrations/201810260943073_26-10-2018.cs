namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26102018 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mailers", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Mailers", new[] { "Id" });
            AddColumn("dbo.Mailers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Id");
            CreateIndex("dbo.Mailers", "ApplicationUser_Id");
            AddForeignKey("dbo.Mailers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mailers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Mailers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropColumn("dbo.Mailers", "ApplicationUser_Id");
            CreateIndex("dbo.Mailers", "Id");
            AddForeignKey("dbo.Mailers", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
