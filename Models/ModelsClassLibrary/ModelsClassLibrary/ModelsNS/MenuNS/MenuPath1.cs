using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.IO;

namespace ModelsClassLibrary.MenuNS
{
    public class MenuPath1 : MenuPathAbstract, IHasUploads
    {

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.MenuPath1;
        }



        public virtual ICollection<ProductCategoryMain> MenuPathMains { get; set; }


        string IHasUploads.MiscFilesLocation()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw);
        }

        public string MiscFilesLocation_Initialization()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY, "menupath");
        }

    }
}