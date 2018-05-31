namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _300520183 : DbMigration
    {
        public override void Up()
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
            
            AddColumn("dbo.Products", "UomWeightListedId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Products", "WeightListed", c => c.Double(nullable: false));
            CreateIndex("dbo.Products", "UomWeightListedId");
            AddForeignKey("dbo.Products", "UomWeightListedId", "dbo.UomWeights", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UomWeightListedId", "dbo.UomWeights");
            DropIndex("dbo.Products", new[] { "UomWeightListedId" });
            DropColumn("dbo.Products", "WeightListed");
            DropColumn("dbo.Products", "UomWeightListedId");
            DropTable("dbo.UomWeights");
        }
    }
}
