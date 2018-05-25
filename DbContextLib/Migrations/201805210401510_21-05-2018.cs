namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21052018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UploadedFiles", "RelativeWebsitePath", c => c.String());
            DropColumn("dbo.UploadedFiles", "RelativePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UploadedFiles", "RelativePath", c => c.String());
            DropColumn("dbo.UploadedFiles", "RelativeWebsitePath");
        }
    }
}
