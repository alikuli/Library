using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Note. Scratch card 16 digit serial number is placed in Name AND in ProductsOwnNumber. I believe thqt ProductsOwnNumber
    /// needs to be removed. No need for that. Name is fine because it will not duplicate intrinsically.
    /// </summary>
    public abstract partial class ProductAbstract : CommonWithId, IProductHasUploads
    {
        /// <summary>
        /// This stores the address of the small picture.
        /// </summary>
        public virtual ICollection<UploadedFile> MiscFiles { get; set; }

        public string MiscFilesLocation()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw);
        }

        public string MiscFilesLocation_Initialization()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY, ClassNameRaw);
        }


        //TODO  Decision to be made. Doe we need to load many different sizes of the same picture, or should we use the ImageResizer to sesize the same picture?
        //Also, should we just store one picture and use the ImageResizer to display the others so we can save space and satisfy the Single Unit principal?

        ///// <summary>
        ///// This stores the address of the big picture.
        ///// </summary>
        //[Display(Name = "Big Picture")]
        //public ICollection<UploadedFile> BigPicture { get; set; }
        //public string BigPictureLocation
        //{
        //    get
        //    {
        //        return AliKuli.ConstantsNS.MyConstants.SAVE_LOCATION_PRODUCT_PICTURE_BIG;
        //    }
        //}



        ///// <summary>
        ///// This stores the address of the medium picture.
        ///// </summary>
        //[Display(Name = "Medium Picture")]
        //public ICollection<UploadedFile> MediumPicture { get; set; }
        //public string MediumPictureLocation
        //{
        //    get
        //    {
        //        return AliKuli.ConstantsNS.MyConstants.SAVE_LOCATION_PRODUCT_PICTURE_MEDIUM;
        //    }
        //}





    }
}