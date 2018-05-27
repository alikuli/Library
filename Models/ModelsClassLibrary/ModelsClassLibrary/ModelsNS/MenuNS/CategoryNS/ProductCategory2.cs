using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterfacesLibrary.SharedNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.IO;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace ModelsClassLibrary.MenuNS

{
    
    public class ProductCategory2:ProductCategoryAbstract, IHasUploads
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return EnumLibrary.EnumNS.ClassesWithRightsENUM.ProductCategory2;
        }
        public void LoadFrom(ProductCategory2 p)
        {
            base.LoadFrom(p as ICommonWithId);
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