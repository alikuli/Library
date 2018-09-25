namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22092018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Counters",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Amount = c.Long(nullable: false),
                        CounterTypeEnum = c.Int(nullable: false),
                        UploadedFileId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.UploadedFiles", t => t.UploadedFileId)
                .Index(t => t.UploadedFileId);
            
            CreateTable(
                "dbo.UploadedFiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        MenuPath1Id = c.String(maxLength: 128),
                        MenuPath2Id = c.String(maxLength: 128),
                        MenuPath3Id = c.String(maxLength: 128),
                        FileDocId = c.String(maxLength: 128),
                        ApplicationUserId = c.String(maxLength: 128),
                        SelfieId = c.String(maxLength: 128),
                        IdCardFrontUploadId = c.String(maxLength: 128),
                        IdCardBackUploadId = c.String(maxLength: 128),
                        PassportFrontUploadId = c.String(maxLength: 128),
                        PassportVisaUploadId = c.String(maxLength: 128),
                        LiscenseFrontUploadId = c.String(maxLength: 128),
                        LiscenseBackUploadId = c.String(maxLength: 128),
                        ProductChildId = c.String(maxLength: 128),
                        OriginalNameWithoutExtention = c.String(),
                        Extention = c.String(),
                        RelativeWebsitePath = c.String(),
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
                .ForeignKey("dbo.FileDocs", t => t.FileDocId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductChilds", t => t.ProductChildId)
                .ForeignKey("dbo.MenuPath3", t => t.MenuPath3Id, cascadeDelete: true)
                .ForeignKey("dbo.MenuPath2", t => t.MenuPath2Id, cascadeDelete: true)
                .ForeignKey("dbo.MenuPath1", t => t.MenuPath1Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.IdCardBackUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.IdCardFrontUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.LiscenseBackUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.LiscenseFrontUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PassportFrontUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.PassportVisaUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.SelfieId)
                .Index(t => t.ProductId)
                .Index(t => t.MenuPath1Id)
                .Index(t => t.MenuPath2Id)
                .Index(t => t.MenuPath3Id)
                .Index(t => t.FileDocId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.SelfieId)
                .Index(t => t.IdCardFrontUploadId)
                .Index(t => t.IdCardBackUploadId)
                .Index(t => t.PassportFrontUploadId)
                .Index(t => t.PassportVisaUploadId)
                .Index(t => t.LiscenseFrontUploadId)
                .Index(t => t.LiscenseBackUploadId)
                .Index(t => t.ProductChildId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CountryId = c.String(maxLength: 128),
                        Input3SortString = c.String(),
                        PhoneNumberAsEntered = c.String(maxLength: 50),
                        CountryIdCardNumber = c.String(),
                        AddressComplex_Name = c.String(maxLength: 2000),
                        AddressComplex_Address2 = c.String(maxLength: 2000),
                        AddressComplex_HouseNo = c.String(maxLength: 2000),
                        AddressComplex_Road = c.String(maxLength: 2000),
                        AddressComplex_WebAddress = c.String(maxLength: 2000),
                        AddressComplex_Zip = c.String(maxLength: 2000),
                        AddressComplex_Phone = c.String(maxLength: 2000),
                        AddressComplex_Attention = c.String(maxLength: 2000),
                        AddressComplex_TownName = c.String(maxLength: 2000),
                        AddressComplex_CityName = c.String(maxLength: 2000),
                        AddressComplex_StateName = c.String(maxLength: 2000),
                        AddressComplex_CountryName = c.String(maxLength: 2000),
                        PersonComplex_IdentificationNo = c.String(maxLength: 50),
                        PersonComplex_FName = c.String(maxLength: 50),
                        PersonComplex_LName = c.String(maxLength: 50),
                        PersonComplex_MName = c.String(maxLength: 50),
                        PersonComplex_Sex = c.Int(nullable: false),
                        PersonComplex_SonOfOrWifeOf = c.Int(nullable: false),
                        PersonComplex_NameOfFatherOrHusband = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        IsBlackListed_Value = c.Boolean(nullable: false),
                        IsBlackListed_MetaData_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsBlackListed_MetaData_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsBlackListed_MetaData_By = c.String(maxLength: 50),
                        IsSuspended_Value = c.Boolean(nullable: false),
                        IsSuspended_MetaData_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsSuspended_MetaData_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsSuspended_MetaData_By = c.String(maxLength: 50),
                        Comment = c.String(),
                        DetailInfoToDisplayOnWebsite = c.String(),
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
                        Name = c.String(),
                        ReturnUrl = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Abbreviation = c.String(maxLength: 10),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Abbreviation = c.String(maxLength: 10),
                        CountryId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.FileDocs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FileNumber = c.Long(nullable: false),
                        OldFileNumber = c.String(),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GlobalComments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        MenuPath1Id = c.String(maxLength: 128),
                        MenuPath2Id = c.String(maxLength: 128),
                        MenuPath3Id = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        ProductChildId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.MenuPath2", t => t.MenuPath2Id)
                .ForeignKey("dbo.MenuPath3", t => t.MenuPath3Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.ProductChilds", t => t.ProductChildId)
                .ForeignKey("dbo.MenuPath1", t => t.MenuPath1Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MenuPath1Id)
                .Index(t => t.MenuPath2Id)
                .Index(t => t.MenuPath3Id)
                .Index(t => t.ProductId)
                .Index(t => t.ProductChildId);
            
            CreateTable(
                "dbo.MenuPath1",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MenuPath1Enum = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                        FeaturesTypeEnum = c.Int(nullable: false),
                        MenuPath1Id = c.String(maxLength: 128),
                        MenuPath2Id = c.String(maxLength: 128),
                        MenuPath3Id = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        ProductChildId = c.String(maxLength: 128),
                        FeatureTypeEnum = c.Int(nullable: false),
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
                .ForeignKey("dbo.MenuPath2", t => t.MenuPath2Id, cascadeDelete: true)
                .ForeignKey("dbo.MenuPath3", t => t.MenuPath3Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductChilds", t => t.ProductChildId)
                .ForeignKey("dbo.MenuPath1", t => t.MenuPath1Id, cascadeDelete: true)
                .Index(t => t.MenuPath1Id)
                .Index(t => t.MenuPath2Id)
                .Index(t => t.MenuPath3Id)
                .Index(t => t.ProductId)
                .Index(t => t.ProductChildId);
            
            CreateTable(
                "dbo.MenuPath2",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LikeUnlikes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MenuPath1Id = c.String(maxLength: 128),
                        MenuPath2Id = c.String(maxLength: 128),
                        MenuPath3Id = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        ProductChildId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        IsLike = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.MenuPath1", t => t.MenuPath1Id)
                .ForeignKey("dbo.MenuPath2", t => t.MenuPath2Id)
                .ForeignKey("dbo.MenuPath3", t => t.MenuPath3Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.ProductChilds", t => t.ProductChildId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.MenuPath1Id)
                .Index(t => t.MenuPath2Id)
                .Index(t => t.MenuPath3Id)
                .Index(t => t.ProductId)
                .Index(t => t.ProductChildId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MenuPath3",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuPathMains",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MenuPath1Id = c.String(maxLength: 128),
                        MenuPath2Id = c.String(maxLength: 128),
                        MenuPath3Id = c.String(maxLength: 128),
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
                .ForeignKey("dbo.MenuPath3", t => t.MenuPath3Id)
                .ForeignKey("dbo.MenuPath2", t => t.MenuPath2Id)
                .ForeignKey("dbo.MenuPath1", t => t.MenuPath1Id)
                .Index(t => t.MenuPath1Id)
                .Index(t => t.MenuPath2Id)
                .Index(t => t.MenuPath3Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NameFieldsData = c.String(),
                        ParentId = c.String(maxLength: 128),
                        Sell_MSRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sell_MlpPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sell_SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Buy_Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDisplayedOnWebsite = c.Boolean(nullable: false),
                        IsSaleable = c.Boolean(nullable: false),
                        UomPurchaseId = c.String(maxLength: 128),
                        UomSaleId = c.String(maxLength: 128),
                        UomVolumeId = c.String(maxLength: 128),
                        Volume = c.Double(nullable: false),
                        UomWeightListedId = c.String(maxLength: 128),
                        WeightListed = c.Double(nullable: false),
                        UomWeightActualId = c.String(maxLength: 128),
                        WeightActual = c.Double(nullable: false),
                        UomDimensionsId = c.String(maxLength: 128),
                        Dimensions_Height = c.Double(nullable: false),
                        Dimensions_Width = c.Double(nullable: false),
                        Dimensions_Length = c.Double(nullable: false),
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
                .ForeignKey("dbo.Products", t => t.ParentId)
                .ForeignKey("dbo.UomLengths", t => t.UomDimensionsId, cascadeDelete: true)
                .ForeignKey("dbo.UomQties", t => t.UomPurchaseId)
                .ForeignKey("dbo.UomQties", t => t.UomSaleId)
                .ForeignKey("dbo.UomVolumes", t => t.UomVolumeId, cascadeDelete: true)
                .ForeignKey("dbo.UomWeights", t => t.UomWeightActualId)
                .ForeignKey("dbo.UomWeights", t => t.UomWeightListedId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.UomPurchaseId)
                .Index(t => t.UomSaleId)
                .Index(t => t.UomVolumeId)
                .Index(t => t.UomWeightListedId)
                .Index(t => t.UomWeightActualId)
                .Index(t => t.UomDimensionsId);
            
            CreateTable(
                "dbo.ProductChilds",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ExpiryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Sell_MSRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sell_MlpPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sell_SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Buy_Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        SerialNumber = c.String(),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductIdentifiers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.UomLengths",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UnitsToMakeOneOfBase = c.Double(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UomQties",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UnitsToMakeOneOfBase = c.Double(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UomVolumes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UnitsToMakeOneOfBase = c.Double(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UomWeights",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UnitsToMakeOneOfBase = c.Double(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Rights",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RightsFor = c.Int(nullable: false),
                        Create = c.Boolean(nullable: false),
                        Retrieve = c.Boolean(nullable: false),
                        Update = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        DeleteActually = c.Boolean(nullable: false),
                        CreateChildren = c.Boolean(nullable: false),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomerCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiscountPrecedences",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Rank = c.Int(nullable: false),
                        DiscountRuleEnum = c.Int(nullable: false),
                        DiscountTypeEnum = c.Int(nullable: false),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OldFileDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompleteFileNumber = c.String(),
                        ParentFileNumber = c.String(),
                        ChildFilenumber = c.Single(nullable: false),
                        ParentDescription = c.String(),
                        ChildNo = c.Single(nullable: false),
                        CategoryName = c.String(),
                        Description = c.String(),
                        FullName = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        FileId = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OwnerCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PageViews",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        UserName = c.String(maxLength: 128),
                        ActionName = c.String(maxLength: 128),
                        ControllerName = c.String(maxLength: 128),
                        HttpMethod = c.String(maxLength: 128),
                        UserHostName = c.String(maxLength: 128),
                        UserHostAddress = c.String(maxLength: 128),
                        UrlRefererrerHost = c.String(maxLength: 128),
                        UserInfo = c.String(maxLength: 128),
                        UserAgent = c.String(),
                        UserLanguages = c.String(maxLength: 128),
                        BrowserType = c.String(maxLength: 128),
                        IsCrawler = c.Boolean(nullable: false),
                        IsMobileDevice = c.Boolean(nullable: false),
                        IsClientWin16Based = c.Boolean(nullable: false),
                        IsClientWin32Based = c.Boolean(nullable: false),
                        IsAjaxRequest = c.Boolean(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentTerms",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NoOfDaysCredit = c.Int(nullable: false),
                        NoOfDaysEarlyPayment = c.Int(nullable: false),
                        EarlyPaymentDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.VisitorLogs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ControllerName = c.String(maxLength: 100),
                        ActionName = c.String(maxLength: 100),
                        PostOrGet = c.String(maxLength: 100),
                        User = c.String(maxLength: 100),
                        Browser = c.String(maxLength: 500),
                        MachineName = c.String(maxLength: 100),
                        Ip = c.String(maxLength: 100),
                        UrlReferrer = c.String(maxLength: 300),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductMenuPathMains",
                c => new
                    {
                        Product_Id = c.String(nullable: false, maxLength: 128),
                        MenuPathMain_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.MenuPathMain_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.MenuPathMains", t => t.MenuPathMain_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.MenuPathMain_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DiscountPrecedences", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Counters", "UploadedFileId", "dbo.UploadedFiles");
            DropForeignKey("dbo.Rights", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "SelfieId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "PassportVisaUploadId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "PassportFrontUploadId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "LiscenseFrontUploadId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "LiscenseBackUploadId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "IdCardFrontUploadId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "IdCardBackUploadId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GlobalComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "MenuPath1Id", "dbo.MenuPath1");
            DropForeignKey("dbo.MenuPathMains", "MenuPath1Id", "dbo.MenuPath1");
            DropForeignKey("dbo.GlobalComments", "MenuPath1Id", "dbo.MenuPath1");
            DropForeignKey("dbo.Features", "MenuPath1Id", "dbo.MenuPath1");
            DropForeignKey("dbo.UploadedFiles", "MenuPath2Id", "dbo.MenuPath2");
            DropForeignKey("dbo.MenuPathMains", "MenuPath2Id", "dbo.MenuPath2");
            DropForeignKey("dbo.LikeUnlikes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "MenuPath3Id", "dbo.MenuPath3");
            DropForeignKey("dbo.MenuPathMains", "MenuPath3Id", "dbo.MenuPath3");
            DropForeignKey("dbo.Products", "UomWeightListedId", "dbo.UomWeights");
            DropForeignKey("dbo.Products", "UomWeightActualId", "dbo.UomWeights");
            DropForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes");
            DropForeignKey("dbo.Products", "UomSaleId", "dbo.UomQties");
            DropForeignKey("dbo.Products", "UomPurchaseId", "dbo.UomQties");
            DropForeignKey("dbo.Products", "UomDimensionsId", "dbo.UomLengths");
            DropForeignKey("dbo.ProductIdentifiers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductChilds", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductChilds", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "ProductChildId", "dbo.ProductChilds");
            DropForeignKey("dbo.LikeUnlikes", "ProductChildId", "dbo.ProductChilds");
            DropForeignKey("dbo.GlobalComments", "ProductChildId", "dbo.ProductChilds");
            DropForeignKey("dbo.Features", "ProductChildId", "dbo.ProductChilds");
            DropForeignKey("dbo.Products", "ParentId", "dbo.Products");
            DropForeignKey("dbo.UploadedFiles", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductMenuPathMains", "MenuPathMain_Id", "dbo.MenuPathMains");
            DropForeignKey("dbo.ProductMenuPathMains", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.LikeUnlikes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.GlobalComments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Features", "ProductId", "dbo.Products");
            DropForeignKey("dbo.LikeUnlikes", "MenuPath3Id", "dbo.MenuPath3");
            DropForeignKey("dbo.GlobalComments", "MenuPath3Id", "dbo.MenuPath3");
            DropForeignKey("dbo.Features", "MenuPath3Id", "dbo.MenuPath3");
            DropForeignKey("dbo.LikeUnlikes", "MenuPath2Id", "dbo.MenuPath2");
            DropForeignKey("dbo.LikeUnlikes", "MenuPath1Id", "dbo.MenuPath1");
            DropForeignKey("dbo.GlobalComments", "MenuPath2Id", "dbo.MenuPath2");
            DropForeignKey("dbo.Features", "MenuPath2Id", "dbo.MenuPath2");
            DropForeignKey("dbo.FileDocs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "FileDocId", "dbo.FileDocs");
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.States", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProductMenuPathMains", new[] { "MenuPathMain_Id" });
            DropIndex("dbo.ProductMenuPathMains", new[] { "Product_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.DiscountPrecedences", new[] { "UserId" });
            DropIndex("dbo.Rights", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.ProductIdentifiers", new[] { "ProductId" });
            DropIndex("dbo.ProductChilds", new[] { "ProductId" });
            DropIndex("dbo.ProductChilds", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "UomDimensionsId" });
            DropIndex("dbo.Products", new[] { "UomWeightActualId" });
            DropIndex("dbo.Products", new[] { "UomWeightListedId" });
            DropIndex("dbo.Products", new[] { "UomVolumeId" });
            DropIndex("dbo.Products", new[] { "UomSaleId" });
            DropIndex("dbo.Products", new[] { "UomPurchaseId" });
            DropIndex("dbo.Products", new[] { "ParentId" });
            DropIndex("dbo.MenuPathMains", new[] { "MenuPath3Id" });
            DropIndex("dbo.MenuPathMains", new[] { "MenuPath2Id" });
            DropIndex("dbo.MenuPathMains", new[] { "MenuPath1Id" });
            DropIndex("dbo.LikeUnlikes", new[] { "UserId" });
            DropIndex("dbo.LikeUnlikes", new[] { "ProductChildId" });
            DropIndex("dbo.LikeUnlikes", new[] { "ProductId" });
            DropIndex("dbo.LikeUnlikes", new[] { "MenuPath3Id" });
            DropIndex("dbo.LikeUnlikes", new[] { "MenuPath2Id" });
            DropIndex("dbo.LikeUnlikes", new[] { "MenuPath1Id" });
            DropIndex("dbo.Features", new[] { "ProductChildId" });
            DropIndex("dbo.Features", new[] { "ProductId" });
            DropIndex("dbo.Features", new[] { "MenuPath3Id" });
            DropIndex("dbo.Features", new[] { "MenuPath2Id" });
            DropIndex("dbo.Features", new[] { "MenuPath1Id" });
            DropIndex("dbo.GlobalComments", new[] { "ProductChildId" });
            DropIndex("dbo.GlobalComments", new[] { "ProductId" });
            DropIndex("dbo.GlobalComments", new[] { "MenuPath3Id" });
            DropIndex("dbo.GlobalComments", new[] { "MenuPath2Id" });
            DropIndex("dbo.GlobalComments", new[] { "MenuPath1Id" });
            DropIndex("dbo.GlobalComments", new[] { "UserId" });
            DropIndex("dbo.FileDocs", new[] { "UserId" });
            DropIndex("dbo.States", new[] { "CountryId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            DropIndex("dbo.UploadedFiles", new[] { "ProductChildId" });
            DropIndex("dbo.UploadedFiles", new[] { "LiscenseBackUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "LiscenseFrontUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "PassportVisaUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "PassportFrontUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "IdCardBackUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "IdCardFrontUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "SelfieId" });
            DropIndex("dbo.UploadedFiles", new[] { "ApplicationUserId" });
            DropIndex("dbo.UploadedFiles", new[] { "FileDocId" });
            DropIndex("dbo.UploadedFiles", new[] { "MenuPath3Id" });
            DropIndex("dbo.UploadedFiles", new[] { "MenuPath2Id" });
            DropIndex("dbo.UploadedFiles", new[] { "MenuPath1Id" });
            DropIndex("dbo.UploadedFiles", new[] { "ProductId" });
            DropIndex("dbo.Counters", new[] { "UploadedFileId" });
            DropTable("dbo.ProductMenuPathMains");
            DropTable("dbo.VisitorLogs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.PaymentTerms");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.PageViews");
            DropTable("dbo.OwnerCategories");
            DropTable("dbo.OldFileDatas");
            DropTable("dbo.Languages");
            DropTable("dbo.DiscountPrecedences");
            DropTable("dbo.CustomerCategories");
            DropTable("dbo.Rights");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.UomWeights");
            DropTable("dbo.UomVolumes");
            DropTable("dbo.UomQties");
            DropTable("dbo.UomLengths");
            DropTable("dbo.ProductIdentifiers");
            DropTable("dbo.ProductChilds");
            DropTable("dbo.Products");
            DropTable("dbo.MenuPathMains");
            DropTable("dbo.MenuPath3");
            DropTable("dbo.LikeUnlikes");
            DropTable("dbo.MenuPath2");
            DropTable("dbo.Features");
            DropTable("dbo.MenuPath1");
            DropTable("dbo.GlobalComments");
            DropTable("dbo.FileDocs");
            DropTable("dbo.States");
            DropTable("dbo.Countries");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UploadedFiles");
            DropTable("dbo.Counters");
        }
    }
}
