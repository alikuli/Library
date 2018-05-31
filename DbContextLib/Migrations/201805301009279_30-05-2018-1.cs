namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _300520181 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes");
            DropIndex("dbo.Products", new[] { "UomVolumeId" });
            AlterColumn("dbo.Products", "UomVolumeId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Products", "UomVolumeId");
            AddForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes");
            DropIndex("dbo.Products", new[] { "UomVolumeId" });
            AlterColumn("dbo.Products", "UomVolumeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "UomVolumeId");
            AddForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes", "Id");
        }
    }
}
