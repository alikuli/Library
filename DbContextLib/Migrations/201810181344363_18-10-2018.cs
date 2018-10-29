namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18102018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mailers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TrustLevelEnum = c.Int(nullable: false),
                        UserId = c.String(),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AddressVerificationHdrs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AddressVerificationTrxId = c.String(),
                        BeginDate_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        BeginDate_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        BeginDate_By = c.String(maxLength: 50),
                        EndDate_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate_By = c.String(maxLength: 50),
                        MailerId = c.String(maxLength: 128),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mailers", t => t.MailerId)
                .Index(t => t.MailerId);
            
            CreateTable(
                "dbo.AddressVerificationTrxes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MailServiceEnum = c.Int(nullable: false),
                        VerificaionStatusEnum = c.Int(nullable: false),
                        SuccessEnum = c.Int(nullable: false),
                        AddressId = c.String(maxLength: 128),
                        AddressVerificationHdrId = c.String(maxLength: 128),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AddressWithIds", t => t.AddressId)
                .ForeignKey("dbo.AddressVerificationHdrs", t => t.AddressVerificationHdrId)
                .Index(t => t.AddressId)
                .Index(t => t.AddressVerificationHdrId);
            
            AddColumn("dbo.AspNetUsers", "MailerId", c => c.String());
            AddColumn("dbo.AddressWithIds", "Verification_Status", c => c.Int(nullable: false));
            DropColumn("dbo.AddressWithIds", "Verification_AddressVerificaionEnum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressWithIds", "Verification_AddressVerificaionEnum", c => c.Int(nullable: false));
            DropForeignKey("dbo.Mailers", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AddressVerificationHdrs", "MailerId", "dbo.Mailers");
            DropForeignKey("dbo.AddressVerificationTrxes", "AddressVerificationHdrId", "dbo.AddressVerificationHdrs");
            DropForeignKey("dbo.AddressVerificationTrxes", "AddressId", "dbo.AddressWithIds");
            DropIndex("dbo.AddressVerificationTrxes", new[] { "AddressVerificationHdrId" });
            DropIndex("dbo.AddressVerificationTrxes", new[] { "AddressId" });
            DropIndex("dbo.AddressVerificationHdrs", new[] { "MailerId" });
            DropIndex("dbo.Mailers", new[] { "Id" });
            DropColumn("dbo.AddressWithIds", "Verification_Status");
            DropColumn("dbo.AspNetUsers", "MailerId");
            DropTable("dbo.AddressVerificationTrxes");
            DropTable("dbo.AddressVerificationHdrs");
            DropTable("dbo.Mailers");
        }
    }
}
