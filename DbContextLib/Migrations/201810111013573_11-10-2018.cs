namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11102018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressWithIds",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        HouseNo = c.String(maxLength: 200),
                        Road = c.String(maxLength: 200),
                        Address2 = c.String(maxLength: 1000),
                        WebAddress = c.String(maxLength: 200),
                        Zip = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 1000),
                        Attention = c.String(maxLength: 200),
                        TownName = c.String(maxLength: 200),
                        CityName = c.String(maxLength: 200),
                        StateName = c.String(maxLength: 200),
                        CountryId = c.Guid(nullable: false),
                        GeoPosition_Latitude = c.String(nullable: false, maxLength: 100),
                        GeoPosition_Longitude = c.String(nullable: false, maxLength: 100),
                        GeoPosition_Created_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        GeoPosition_Created_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        GeoPosition_Created_By = c.String(maxLength: 50),
                        GeoPosition_Modified_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        GeoPosition_Modified_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        GeoPosition_Modified_By = c.String(maxLength: 50),
                        AddressType_ShipTo = c.Boolean(nullable: false),
                        AddressType_InformTo = c.Boolean(nullable: false),
                        AddressType_BillTo = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Comment = c.String(maxLength: 1000),
                        DetailInfoToDisplayOnWebsite = c.String(),
                        Name = c.String(),
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
                        Country_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Country_Id);
            
            DropColumn("dbo.AspNetUsers", "AddressComplex_Name");
            DropColumn("dbo.AspNetUsers", "AddressComplex_CountryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AddressComplex_CountryName", c => c.String(maxLength: 2000));
            AddColumn("dbo.AspNetUsers", "AddressComplex_Name", c => c.String(maxLength: 2000));
            DropForeignKey("dbo.AddressWithIds", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AddressWithIds", "Country_Id", "dbo.Countries");
            DropIndex("dbo.AddressWithIds", new[] { "Country_Id" });
            DropIndex("dbo.AddressWithIds", new[] { "UserId" });
            DropTable("dbo.AddressWithIds");
        }
    }
}
