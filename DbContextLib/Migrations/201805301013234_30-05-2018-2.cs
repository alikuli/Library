namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _300520182 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "UomWeight_Id", "dbo.UomWeights");
            DropForeignKey("dbo.Products", "UomWeightActualId", "dbo.UomWeights");
            DropForeignKey("dbo.Products", "UomWeightListedId", "dbo.UomWeights");
            DropForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes");
            DropIndex("dbo.Products", new[] { "UomVolumeId" });
            DropIndex("dbo.Products", new[] { "UomWeightListedId" });
            DropIndex("dbo.Products", new[] { "UomWeightActualId" });
            DropIndex("dbo.Products", new[] { "UomWeight_Id" });
            AlterColumn("dbo.Products", "UomVolumeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "UomVolumeId");
            AddForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes", "Id");
            DropColumn("dbo.Products", "UomWeightListedId");
            DropColumn("dbo.Products", "WeightListed");
            DropColumn("dbo.Products", "UomWeightActualId");
            DropColumn("dbo.Products", "WeightActual");
            DropColumn("dbo.Products", "UomWeight_Id");
            DropTable("dbo.UomWeights");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UomWeights",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UnitsToMakeOneOfBase = c.Double(nullable: false),
                        Comment = c.String(maxLength: 1000),
                        DetailInfoToDisplayOnWebsite = c.String(),
                        Name = c.String(maxLength: 2000),
                        MetaData_IsEditLocked = c.Boolean(nullable: false),
                        MetaData_IsActive = c.Boolean(nullable: false),
                        MetaData_IsDeleted = c.Boolean(nullable: false),
                        MetaData_Created_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Created_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Created_By = c.String(maxLength: 50),
                        MetaData_Deleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Deleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Deleted_By = c.String(maxLength: 50),
                        MetaData_Modified_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Modified_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_Modified_By = c.String(maxLength: 50),
                        MetaData_UnDeleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_UnDeleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        MetaData_UnDeleted_By = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "UomWeight_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "WeightActual", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "UomWeightActualId", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "WeightListed", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "UomWeightListedId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes");
            DropIndex("dbo.Products", new[] { "UomVolumeId" });
            AlterColumn("dbo.Products", "UomVolumeId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Products", "UomWeight_Id");
            CreateIndex("dbo.Products", "UomWeightActualId");
            CreateIndex("dbo.Products", "UomWeightListedId");
            CreateIndex("dbo.Products", "UomVolumeId");
            AddForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "UomWeightListedId", "dbo.UomWeights", "Id");
            AddForeignKey("dbo.Products", "UomWeightActualId", "dbo.UomWeights", "Id");
            AddForeignKey("dbo.Products", "UomWeight_Id", "dbo.UomWeights", "Id");
        }
    }
}
