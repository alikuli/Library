using AliKuli.Extentions;
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
using System.Linq;

namespace ModelsClassLibrary.MenuNS
{

    public class MenuPath3 : MenuPathAbstract, IHasUploads, IMenuPath, IHasMenuPaths
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


        [NotMapped]

        public List<UploadedFile> MiscFiles_Fixed
        {
            get
            {
                if (MiscFiles.IsNullOrEmpty())
                    return new List<UploadedFile>();

                List<UploadedFile> miscFile = MiscFiles.Where(x => x.MetaData.IsDeleted == false).ToList();
                return miscFile;
            }
        }
        [NotMapped]
        public List<MenuPathMain> MenuPathMains_Fixed
        {
            get
            {
                if (MenuPathMains.IsNullOrEmpty())
                    return new List<MenuPathMain>();

                List<MenuPathMain> lst = MenuPathMains.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }
        [NotMapped]
        public List<GlobalComment> GlobalComments_Fixed
        {
            get
            {
                if (GlobalComments.IsNullOrEmpty())
                    return new List<GlobalComment>();

                List<GlobalComment> lst = GlobalComments.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }
        [NotMapped]
        public List<LikeUnlike> LikeUnlikes_Fixed
        {
            get
            {
                if (LikeUnlikes.IsNullOrEmpty())
                    return new List<LikeUnlike>();

                List<LikeUnlike> lst = LikeUnlikes.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }
        [NotMapped]
        public List<Message> Messages_Fixed
        {
            get
            {
                if (Messages.IsNullOrEmpty())
                    return new List<Message>();

                List<Message> lst = Messages.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }
        [NotMapped]
        public List<MenuFeature> MenuFeatures_Fixed
        {
            get
            {
                if (MenuFeatures.IsNullOrEmpty())
                    return new List<MenuFeature>();

                List<MenuFeature> lst = MenuFeatures.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }

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