using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.IO;

namespace ModelsClassLibrary.MenuNS
{

    public class MenuPath3 : MenuPathAbstract, IHasUploads
    {

        public virtual ICollection<UploadedFile> MiscFiles { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.MenuPath3;
        }
        public void LoadFrom(MenuPath3 p)
        {
            base.LoadFrom(p as ICommonWithId);
        }


        public virtual ICollection<MenuPathMain> MenuPathMains { get; set; }

        string IHasUploads.MiscFilesLocation()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw);

        }

        public string MiscFilesLocation_Initialization()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY, "MenuPaths");
        }

    }
}