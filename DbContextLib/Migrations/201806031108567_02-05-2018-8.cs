namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020520188 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UploadedFiles", new[] { "ProductChildId" });
            AlterColumn("dbo.UploadedFiles", "ProductChildId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UploadedFiles", "ProductChildId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UploadedFiles", new[] { "ProductChildId" });
            AlterColumn("dbo.UploadedFiles", "ProductChildId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UploadedFiles", "ProductChildId");
        }
    }
}
