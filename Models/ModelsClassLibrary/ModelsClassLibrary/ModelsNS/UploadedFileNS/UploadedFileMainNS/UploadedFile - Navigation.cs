using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using UserModels;
namespace ModelsClassLibrary.ModelsNS.UploadedFileNS
{
    /// <summary>
    /// Note the new name of the file will be in Name in the website, without the extention. This stores the old and the new name along with the extention.
    /// This automatically saves the files to the disk, i.e. in the constructor.
    /// </summary>
    public partial class UploadedFile : CommonWithId
    {


        [Display(Name = "Menu Path 1")]
        public virtual string MenuPath1Id { get; set; }
        public virtual MenuPath1 MenuPath1 { get; set; }



        [Display(Name = "Menu Path 2")]
        public virtual string MenuPath2Id { get; set; }
        public virtual MenuPath2 MenuPath2 { get; set; }




        [Display(Name = "Category 3")]
        public virtual string ProductCategory3Id { get; set; }
        public virtual ProductCategory3 ProductCategory3 { get; set; }



        [Display(Name = "File Documents")]
        public virtual string FileDocId { get; set; }
        public virtual FileDoc FileDoc { get; set; }


        [Display(Name = "Misc Uploads")]
        public virtual string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        [Display(Name = "Selfie")]
        public virtual string SelfieId { get; set; }
        public virtual ApplicationUser Selfie { get; set; }




        [Display(Name = "ID Card Front")]
        public virtual string IdCardFrontUploadId { get; set; }
        public virtual ApplicationUser IdCardFrontUpload { get; set; }


        [Display(Name = "ID Card Back")]
        public virtual string IdCardBackUploadId { get; set; }
        public virtual ApplicationUser IdCardBackUpload { get; set; }

        [Display(Name = "Passport Front")]
        public virtual string PassportFrontUploadId { get; set; }
        public virtual ApplicationUser PassportFrontUpload { get; set; }

        [Display(Name = "Passport Back")]
        public virtual string PassportVisaUploadId { get; set; }
        public virtual ApplicationUser PassportVisaUpload { get; set; }

        [Display(Name = "Liscense Front")]
        public virtual string LiscenseFrontUploadId { get; set; }
        public virtual ApplicationUser LiscenseFrontUpload { get; set; }

        [Display(Name = "Liscense Back")]
        public virtual string LiscenseBackUploadId { get; set; }
        public virtual ApplicationUser LiscenseBackUpload { get; set; }



        #region Product

        /// <summary>
        /// This is the Misc file.
        /// </summary>
        //[Display(Name = "Small Pic")]
        //public virtual string ProductSmallPicId { get; set; }
        //public virtual Product ProductSmallPic { get; set; }


        //[Display(Name = "Medium Pic")]
        //public virtual string ProductMediumPicId { get; set; }
        //public virtual Product ProductMediumPic { get; set; }

        //[Display(Name = "Big Pic")]
        //public virtual string ProductBigPicId { get; set; }
        //public virtual Product ProductBigPic { get; set; }
        #endregion

    }
}
