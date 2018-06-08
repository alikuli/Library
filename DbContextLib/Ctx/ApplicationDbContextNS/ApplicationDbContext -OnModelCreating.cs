using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Data.Entity;
using UserModels;

namespace ApplicationDbContextNS
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {



            base.OnModelCreating(modelBuilder);

            //This causes the uploads to be deleted along with the main file.
            //we need to delete the physical uploads seperately
            #region Uploads

            modelBuilder.Entity<MenuPath1>()
                .HasMany<UploadedFile>(x => x.MiscFiles)
                .WithOptional(x => x.MenuPath1)
                .HasForeignKey(x => x.MenuPath1Id)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<MenuPath2>()
                .HasMany<UploadedFile>(x => x.MiscFiles)
                .WithOptional(x => x.MenuPath2)
                .HasForeignKey(x => x.MenuPath2Id)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<MenuPath3>()
                .HasMany<UploadedFile>(x => x.MiscFiles)
                .WithOptional(x => x.MenuPath3)
                .HasForeignKey(x => x.MenuPath3Id)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<FileDoc>()
                .HasMany<UploadedFile>(x => x.MiscFiles)
                .WithOptional(x => x.FileDoc)
                .HasForeignKey(x => x.FileDocId)
                .WillCascadeOnDelete(true);


            //USER Images
            modelBuilder.Entity<ApplicationUser>()
                .HasMany<UploadedFile>(x => x.MiscFiles)
                .WithOptional(x => x.ApplicationUser)
                .HasForeignKey(x => x.ApplicationUserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<UploadedFile>(x => x.SelfieUploads)
                .WithOptional(x => x.Selfie)
                .HasForeignKey(x => x.SelfieId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany<UploadedFile>(x => x.IdCardFrontUploads)
                .WithOptional(x => x.IdCardFrontUpload)
                .HasForeignKey(x => x.IdCardFrontUploadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<UploadedFile>(x => x.IdCardBackUploads)
                .WithOptional(x => x.IdCardBackUpload)
                .HasForeignKey(x => x.IdCardBackUploadId)
                .WillCascadeOnDelete(false);



            modelBuilder.Entity<ApplicationUser>()
                .HasMany<UploadedFile>(x => x.PassportFrontUploads)
                .WithOptional(x => x.PassportFrontUpload)
                .HasForeignKey(x => x.PassportFrontUploadId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany<UploadedFile>(x => x.PassportVisaUploads)
                .WithOptional(x => x.PassportVisaUpload)
                .HasForeignKey(x => x.PassportVisaUploadId)
                .WillCascadeOnDelete(false);



            modelBuilder.Entity<ApplicationUser>()
                .HasMany<UploadedFile>(x => x.LiscenseFrontUploads)
                .WithOptional(x => x.LiscenseFrontUpload)
                .HasForeignKey(x => x.LiscenseFrontUploadId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany<UploadedFile>(x => x.LiscenseBackUploads)
                .WithOptional(x => x.LiscenseBackUpload)
                .HasForeignKey(x => x.LiscenseBackUploadId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ProductChild>()
                .HasMany<UploadedFile>(x => x.MiscFiles)
                .WithOptional(x => x.ProductChild)
                .HasForeignKey(x => x.ProductChildId)
                .WillCascadeOnDelete(false);

            #endregion

            #region Menu Paths

            modelBuilder.Entity<MenuPath1>()
                .HasMany<MenuPathMain>(x => x.MenuPathMains)
                .WithOptional(x => x.MenuPath1)
                .HasForeignKey(x => x.MenuPath1Id)
                .WillCascadeOnDelete(false);



            modelBuilder.Entity<MenuPath2>()
                .HasMany<MenuPathMain>(x => x.MenuPathMains)
                .WithOptional(x => x.MenuPath2)
                .HasForeignKey(x => x.MenuPath2Id)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<MenuPath3>()
                .HasMany<MenuPathMain>(x => x.MenuPathMains)
                .WithOptional(x => x.MenuPath3)
                .HasForeignKey(x => x.MenuPath3Id)
                .WillCascadeOnDelete(false);


            #endregion

            #region Product

            modelBuilder.Entity<Product>()
                .HasOptional<UomLength>(x => x.UomDimensions)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.UomDimensionsId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Product>()
                .HasOptional<UomQty>(x => x.UomPurchase)
                .WithMany(x => x.Products_Purchase)
                .HasForeignKey(x => x.UomPurchaseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasOptional<UomQty>(x => x.UomSale)
                .WithMany(x => x.Products_Sale)
                .HasForeignKey(x => x.UomSaleId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Product>()
                .HasOptional<UomVolume>(x => x.UomVolume)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.UomVolumeId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Product>()
                .HasOptional<UomWeight>(x => x.UomWeightActual)
                .WithMany(x => x.Products_WeightActual)
                .HasForeignKey(x => x.UomWeightActualId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Product>()
                .HasOptional<UomWeight>(x => x.UomWeightListed)
                .WithMany(x => x.Products_WeightListed)
                .HasForeignKey(x => x.UomWeightListedId)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<Product>()
                .HasMany<ProductIdentifier>(x => x.ProductIdentifiers)
                .WithOptional(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<Product>()
                .HasMany<UploadedFile>(x => x.MiscFiles)
                .WithOptional(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Product>()
                .HasMany<ProductChild>(x => x.ProductChildren)
                .WithOptional(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProductChild>()
                .HasOptional<ApplicationUser>(x => x.User)
                .WithMany(x => x.ProductChildren)
                .WillCascadeOnDelete(true);
            #endregion


            //modelBuilder.Entity<FileDoc>()
            //    .HasOptional<ApplicationUser>(x => x.User)
            //    .WithMany(x => x.FileDocs)
            //    .HasForeignKey(x => x.UserId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<FileDoc>(x => x.FileDocs)
                .WithOptional(x => x.User)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

                
            //modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");


            //modelBuilder.Entity<User>()
            //    .HasKey(x => x.Id)
            //    .HasOptional(x => x.Address)
            //    .WithRequired(x => x.User)
            //    .WillCascadeOnDelete(true);

        }


    }
}