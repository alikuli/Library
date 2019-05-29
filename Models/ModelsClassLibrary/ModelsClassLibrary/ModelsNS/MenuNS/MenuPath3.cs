using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace ModelsClassLibrary.MenuNS
{

    public class MenuPath3 : MenuPathAbstract, IHasUploads, IMenuPath
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
        public virtual ICollection<GlobalComment> GlobalComments { get; set; }
        public virtual ICollection<LikeUnlike> LikeUnlikes { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<MenuFeature> MenuFeatures { get; set; }

        string IHasUploads.MiscFilesLocation(string aName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw);

        }

        public string MiscFilesLocation_Initialization()
        {
            return AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;
        }


        [NotMapped]
        public string MenuPath1Id_Parent { get; set; }

        [NotMapped]
        public string MenuPath2Id_Parent { get; set; }
    }
}