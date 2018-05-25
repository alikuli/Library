namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Abbreviation = c.String(maxLength: 10),
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
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Abbreviation = c.String(maxLength: 10),
                        CountryId = c.String(maxLength: 128),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Input3SortString = c.String(),
                        CountryId = c.String(maxLength: 128),
                        PhoneNumberAsEntered = c.String(maxLength: 50),
                        CountryIdCardNumber = c.String(),
                        AddressComplex_Address2 = c.String(maxLength: 1000),
                        AddressComplex_HouseNo = c.String(maxLength: 1000),
                        AddressComplex_Road = c.String(maxLength: 1000),
                        AddressComplex_WebAddress = c.String(maxLength: 1000),
                        AddressComplex_Zip = c.String(maxLength: 1000),
                        AddressComplex_Phone = c.String(maxLength: 100),
                        AddressComplex_Attention = c.String(maxLength: 1000),
                        AddressComplex_TownName = c.String(maxLength: 1000),
                        AddressComplex_CityName = c.String(maxLength: 1000),
                        AddressComplex_StateName = c.String(maxLength: 1000),
                        AddressComplex_CountryName = c.String(),
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
                "dbo.UploadedFiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OriginalNameWithoutExtention = c.String(),
                        Extention = c.String(),
                        RelativePath = c.String(),
                        ProductCategory1Id = c.String(maxLength: 128),
                        ProductCategory2Id = c.String(maxLength: 128),
                        ProductCategory3Id = c.String(maxLength: 128),
                        FileDocId = c.String(maxLength: 128),
                        ApplicationUserId = c.String(maxLength: 128),
                        SelfieId = c.String(maxLength: 128),
                        IdCardFrontUploadId = c.String(maxLength: 128),
                        IdCardBackUploadId = c.String(maxLength: 128),
                        PassportFrontUploadId = c.String(maxLength: 128),
                        PassportVisaUploadId = c.String(maxLength: 128),
                        LiscenseFrontUploadId = c.String(maxLength: 128),
                        LiscenseBackUploadId = c.String(maxLength: 128),
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
                        ProductCategoryMain_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileDocs", t => t.FileDocId, cascadeDelete: true)
                .ForeignKey("dbo.ProductCategory1", t => t.ProductCategory1Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductCategoryMains", t => t.ProductCategoryMain_Id)
                .ForeignKey("dbo.ProductCategory2", t => t.ProductCategory2Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductCategory3", t => t.ProductCategory3Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.IdCardBackUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.IdCardFrontUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.LiscenseBackUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.LiscenseFrontUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PassportFrontUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.PassportVisaUploadId)
                .ForeignKey("dbo.AspNetUsers", t => t.SelfieId)
                .Index(t => t.ProductCategory1Id)
                .Index(t => t.ProductCategory2Id)
                .Index(t => t.ProductCategory3Id)
                .Index(t => t.FileDocId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.SelfieId)
                .Index(t => t.IdCardFrontUploadId)
                .Index(t => t.IdCardBackUploadId)
                .Index(t => t.PassportFrontUploadId)
                .Index(t => t.PassportVisaUploadId)
                .Index(t => t.LiscenseFrontUploadId)
                .Index(t => t.LiscenseBackUploadId)
                .Index(t => t.ProductCategoryMain_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProductCategory1",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            
            CreateTable(
                "dbo.ProductCategoryMains",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProductCat1Id = c.String(maxLength: 128),
                        ProductCat2Id = c.String(maxLength: 128),
                        ProductCat3Id = c.String(maxLength: 128),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategory2", t => t.ProductCat2Id)
                .ForeignKey("dbo.ProductCategory3", t => t.ProductCat3Id)
                .ForeignKey("dbo.ProductCategory1", t => t.ProductCat1Id)
                .Index(t => t.ProductCat1Id)
                .Index(t => t.ProductCat2Id)
                .Index(t => t.ProductCat3Id);
            
            CreateTable(
                "dbo.ProductCategory2",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            
            CreateTable(
                "dbo.ProductCategory3",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            
            CreateTable(
                "dbo.ProductIdentifiers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                        Product_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Date_ExpiryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProdCategoryID = c.String(maxLength: 128),
                        ParentId = c.String(maxLength: 128),
                        Sell_MSRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sell_MlpPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sell_SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Buy_Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SerialNo = c.String(),
                        BigSizePictureAddress = c.String(maxLength: 1000),
                        MediumSizePictureAddress = c.String(maxLength: 1000),
                        SmallPictureUploadAddress = c.String(maxLength: 1000),
                        IsUomForPackingVolRequired = c.Boolean(nullable: false),
                        IsAllowd_Zero_MRSP = c.Boolean(nullable: false),
                        IsChild = c.Boolean(nullable: false),
                        IsDisplayedOnWebsite = c.Boolean(nullable: false),
                        UomPurchaseId = c.String(),
                        UomStockID = c.String(),
                        Qty_LastQtyError = c.Double(nullable: false),
                        Qty_LastUomStock_UnitsToMakeOneOfBase = c.Double(nullable: false),
                        Qty_LastUomStock_Id = c.String(),
                        Qty_LastUomStock_Comment = c.String(maxLength: 1000),
                        Qty_LastUomStock_DetailInfoToDisplayOnWebsite = c.String(),
                        Qty_LastUomStock_Name = c.String(maxLength: 2000),
                        Qty_LastUomStock_MetaData_IsEditLocked = c.Boolean(nullable: false),
                        Qty_LastUomStock_MetaData_IsActive = c.Boolean(nullable: false),
                        Qty_LastUomStock_MetaData_IsDeleted = c.Boolean(nullable: false),
                        Qty_LastUomStock_MetaData_Created_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_LastUomStock_MetaData_Created_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_LastUomStock_MetaData_Created_By = c.String(maxLength: 50),
                        Qty_LastUomStock_MetaData_Deleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_LastUomStock_MetaData_Deleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_LastUomStock_MetaData_Deleted_By = c.String(maxLength: 50),
                        Qty_LastUomStock_MetaData_Modified_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_LastUomStock_MetaData_Modified_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_LastUomStock_MetaData_Modified_By = c.String(maxLength: 50),
                        Qty_LastUomStock_MetaData_UnDeleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_LastUomStock_MetaData_UnDeleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_LastUomStock_MetaData_UnDeleted_By = c.String(maxLength: 50),
                        Qty_LastUomStockID = c.Long(nullable: false),
                        Qty_LastQtyErrorDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Qty_PreviousToLastQtyError = c.Double(nullable: false),
                        Qty_PreviousToLastUomStock_UnitsToMakeOneOfBase = c.Double(nullable: false),
                        Qty_PreviousToLastUomStock_Id = c.String(),
                        Qty_PreviousToLastUomStock_Comment = c.String(maxLength: 1000),
                        Qty_PreviousToLastUomStock_DetailInfoToDisplayOnWebsite = c.String(),
                        Qty_PreviousToLastUomStock_Name = c.String(maxLength: 2000),
                        Qty_PreviousToLastUomStock_MetaData_IsEditLocked = c.Boolean(nullable: false),
                        Qty_PreviousToLastUomStock_MetaData_IsActive = c.Boolean(nullable: false),
                        Qty_PreviousToLastUomStock_MetaData_IsDeleted = c.Boolean(nullable: false),
                        Qty_PreviousToLastUomStock_MetaData_Created_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_PreviousToLastUomStock_MetaData_Created_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_PreviousToLastUomStock_MetaData_Created_By = c.String(maxLength: 50),
                        Qty_PreviousToLastUomStock_MetaData_Deleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_PreviousToLastUomStock_MetaData_Deleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_PreviousToLastUomStock_MetaData_Deleted_By = c.String(maxLength: 50),
                        Qty_PreviousToLastUomStock_MetaData_Modified_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_PreviousToLastUomStock_MetaData_Modified_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_PreviousToLastUomStock_MetaData_Modified_By = c.String(maxLength: 50),
                        Qty_PreviousToLastUomStock_MetaData_UnDeleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_PreviousToLastUomStock_MetaData_UnDeleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_PreviousToLastUomStock_MetaData_UnDeleted_By = c.String(maxLength: 50),
                        Qty_PreviousToLastUomStockID = c.Long(nullable: false),
                        Qty_PreviousToLastQtyErrorDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Qty_B4PreviousToLastQtyError = c.Double(nullable: false),
                        Qty_B4PreviousToLastUomStock_UnitsToMakeOneOfBase = c.Double(nullable: false),
                        Qty_B4PreviousToLastUomStock_Id = c.String(),
                        Qty_B4PreviousToLastUomStock_Comment = c.String(maxLength: 1000),
                        Qty_B4PreviousToLastUomStock_DetailInfoToDisplayOnWebsite = c.String(),
                        Qty_B4PreviousToLastUomStock_Name = c.String(maxLength: 2000),
                        Qty_B4PreviousToLastUomStock_MetaData_IsEditLocked = c.Boolean(nullable: false),
                        Qty_B4PreviousToLastUomStock_MetaData_IsActive = c.Boolean(nullable: false),
                        Qty_B4PreviousToLastUomStock_MetaData_IsDeleted = c.Boolean(nullable: false),
                        Qty_B4PreviousToLastUomStock_MetaData_Created_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_B4PreviousToLastUomStock_MetaData_Created_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_B4PreviousToLastUomStock_MetaData_Created_By = c.String(maxLength: 50),
                        Qty_B4PreviousToLastUomStock_MetaData_Deleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_B4PreviousToLastUomStock_MetaData_Deleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_B4PreviousToLastUomStock_MetaData_Deleted_By = c.String(maxLength: 50),
                        Qty_B4PreviousToLastUomStock_MetaData_Modified_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_B4PreviousToLastUomStock_MetaData_Modified_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_B4PreviousToLastUomStock_MetaData_Modified_By = c.String(maxLength: 50),
                        Qty_B4PreviousToLastUomStock_MetaData_UnDeleted_DateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_B4PreviousToLastUomStock_MetaData_UnDeleted_Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Qty_B4PreviousToLastUomStock_MetaData_UnDeleted_By = c.String(maxLength: 50),
                        Qty_B4PreviousToLastUomStockID = c.Long(nullable: false),
                        Qty_B4PreviousToLastQtyErrorDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UomWeightId = c.String(),
                        Weight = c.Double(nullable: false),
                        UomVolumeId = c.String(maxLength: 128),
                        Volume = c.Double(nullable: false),
                        UomShipWeightId = c.String(maxLength: 128),
                        ShipWeight = c.Double(nullable: false),
                        UomPackageLengthId = c.String(maxLength: 128),
                        Dims_Height = c.Double(nullable: false),
                        Dims_Width = c.Double(nullable: false),
                        Dims_Length = c.Double(nullable: false),
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
                        UomUomWeight_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ParentId)
                .ForeignKey("dbo.ProductCategoryMains", t => t.ProdCategoryID)
                .ForeignKey("dbo.UomLengths", t => t.UomPackageLengthId)
                .ForeignKey("dbo.UomWeights", t => t.UomShipWeightId)
                .ForeignKey("dbo.UomWeights", t => t.UomUomWeight_Id)
                .ForeignKey("dbo.UomVolumes", t => t.UomVolumeId)
                .Index(t => t.ProdCategoryID)
                .Index(t => t.ParentId)
                .Index(t => t.UomVolumeId)
                .Index(t => t.UomShipWeightId)
                .Index(t => t.UomPackageLengthId)
                .Index(t => t.UomUomWeight_Id);
            
            CreateTable(
                "dbo.UomLengths",
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
            
            CreateTable(
                "dbo.UomVolumes",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Products", "UomVolumeId", "dbo.UomVolumes");
            DropForeignKey("dbo.Products", "UomUomWeight_Id", "dbo.UomWeights");
            DropForeignKey("dbo.Products", "UomShipWeightId", "dbo.UomWeights");
            DropForeignKey("dbo.Products", "UomPackageLengthId", "dbo.UomLengths");
            DropForeignKey("dbo.Products", "ProdCategoryID", "dbo.ProductCategoryMains");
            DropForeignKey("dbo.Products", "ParentId", "dbo.Products");
            DropForeignKey("dbo.ProductIdentifiers", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.DiscountPrecedences", "UserId", "dbo.AspNetUsers");
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
            DropForeignKey("dbo.ProductCategoryMains", "ProductCat1Id", "dbo.ProductCategory1");
            DropForeignKey("dbo.ProductCategoryMains", "ProductCat3Id", "dbo.ProductCategory3");
            DropForeignKey("dbo.UploadedFiles", "ProductCategory3Id", "dbo.ProductCategory3");
            DropForeignKey("dbo.ProductCategoryMains", "ProductCat2Id", "dbo.ProductCategory2");
            DropForeignKey("dbo.UploadedFiles", "ProductCategory2Id", "dbo.ProductCategory2");
            DropForeignKey("dbo.UploadedFiles", "ProductCategoryMain_Id", "dbo.ProductCategoryMains");
            DropForeignKey("dbo.UploadedFiles", "ProductCategory1Id", "dbo.ProductCategory1");
            DropForeignKey("dbo.FileDocs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UploadedFiles", "FileDocId", "dbo.FileDocs");
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.States", "CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Products", new[] { "UomUomWeight_Id" });
            DropIndex("dbo.Products", new[] { "UomPackageLengthId" });
            DropIndex("dbo.Products", new[] { "UomShipWeightId" });
            DropIndex("dbo.Products", new[] { "UomVolumeId" });
            DropIndex("dbo.Products", new[] { "ParentId" });
            DropIndex("dbo.Products", new[] { "ProdCategoryID" });
            DropIndex("dbo.ProductIdentifiers", new[] { "Product_Id" });
            DropIndex("dbo.DiscountPrecedences", new[] { "UserId" });
            DropIndex("dbo.Rights", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.ProductCategoryMains", new[] { "ProductCat3Id" });
            DropIndex("dbo.ProductCategoryMains", new[] { "ProductCat2Id" });
            DropIndex("dbo.ProductCategoryMains", new[] { "ProductCat1Id" });
            DropIndex("dbo.FileDocs", new[] { "UserId" });
            DropIndex("dbo.UploadedFiles", new[] { "ProductCategoryMain_Id" });
            DropIndex("dbo.UploadedFiles", new[] { "LiscenseBackUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "LiscenseFrontUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "PassportVisaUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "PassportFrontUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "IdCardBackUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "IdCardFrontUploadId" });
            DropIndex("dbo.UploadedFiles", new[] { "SelfieId" });
            DropIndex("dbo.UploadedFiles", new[] { "ApplicationUserId" });
            DropIndex("dbo.UploadedFiles", new[] { "FileDocId" });
            DropIndex("dbo.UploadedFiles", new[] { "ProductCategory3Id" });
            DropIndex("dbo.UploadedFiles", new[] { "ProductCategory2Id" });
            DropIndex("dbo.UploadedFiles", new[] { "ProductCategory1Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            DropIndex("dbo.States", new[] { "CountryId" });
            DropTable("dbo.VisitorLogs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UomVolumes");
            DropTable("dbo.UomWeights");
            DropTable("dbo.UomLengths");
            DropTable("dbo.Products");
            DropTable("dbo.ProductIdentifiers");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.PaymentTerms");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.OwnerCategories");
            DropTable("dbo.OldFileDatas");
            DropTable("dbo.Languages");
            DropTable("dbo.DiscountPrecedences");
            DropTable("dbo.CustomerCategories");
            DropTable("dbo.Rights");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.ProductCategory3");
            DropTable("dbo.ProductCategory2");
            DropTable("dbo.ProductCategoryMains");
            DropTable("dbo.ProductCategory1");
            DropTable("dbo.FileDocs");
            DropTable("dbo.UploadedFiles");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.States");
            DropTable("dbo.Countries");
        }
    }
}
