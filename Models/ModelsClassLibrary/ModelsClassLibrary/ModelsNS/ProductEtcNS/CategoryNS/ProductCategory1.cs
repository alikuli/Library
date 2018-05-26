using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.IO;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public class ProductCategory1 : ProductCategoryAbstract, IHasUploads
    {

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.ProductCategory1;
        }



        public virtual ICollection<ProductCategoryMain> ProductCategoryMains { get; set; }


        string IHasUploads.MiscFilesLocation()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw);
        }

        public string MiscFilesLocation_Initialization()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY, "productcategory");
        }

    }
}