using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.ProductEtcNS
{
    public class ProductChild : CommonWithId, IHasUploads
    {
        public ProductChild()
        {
            Sell = new SalePriceComplex();
            Buy = new CostsComplex();
        }

        /// <summary>
        /// This is the User who owns this product. This needs to be set at the time of creating because we may want to upload all this stuff in their own directory
        /// </summary>
        [Display(Name = "Owner")]
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.ProductChild; ;
        }
        public override string ClassNamePlural
        {
            get
            {
                return "ProductChildren";
            }
        }
        #region IHasUploads
        public System.Collections.Generic.ICollection<UploadedFile> MiscFiles { get; set; }

        public string MiscFilesLocation()
        {
            if (Owner.IsNull())
            {
                throw new Exception("The Owner is null. This cannot be null and should be set at creation! Programming error");
            }

            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw, Owner.UserName);
        }

        public string MiscFilesLocation_Initialization()
        {
            if (Owner.IsNull())
            {
                throw new Exception("The Owner is null. This cannot be null and should be set at creation! Programming error");
            }
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY, ClassNameRaw, Owner.UserName);
        }
        #endregion

        #region Sale and Purchase
        public SalePriceComplex Sell { get; set; }

        public CostsComplex Buy { get; set; }




        #endregion


        [Column(TypeName = "DateTime2")]
        [Display(Name = "Expirey(UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }


        /// <summary>
        /// This is the owning product.
        /// </summary>
        [Display(Name = "Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }





}
