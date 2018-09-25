using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ModelsClassLibrary.MenuNS
{
    public class MenuPath1 : MenuPathAbstract, IHasUploads
    {
        public MenuPath1()
        {
            MenuPath1Enum = MenuPath1ENUM.NotDefined;
        }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.MenuPath1;
        }
        public virtual ICollection<UploadedFile> MiscFiles { get; set; }
        public virtual ICollection<MenuPathMain> MenuPathMains { get; set; }
        public virtual ICollection<GlobalComment> GlobalComments { get; set; }
        public virtual ICollection<LikeUnlike> LikeUnlikes { get; set; }
        public virtual ICollection<MenuPath1Feature> MenuPath1Features { get; set; }


        string IHasUploads.MiscFilesLocation()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw);
        }

        public string MiscFilesLocation_Initialization()
        {
            return AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;
        }

        [Display(Name = "Menu Path 1")]
        public MenuPath1ENUM MenuPath1Enum { get; set; }

        public string CreateNameFromEnum(MenuPath1ENUM e)
        {
            return Enum.GetName(typeof(MenuPath1ENUM), e).ToTitleSentance();
        }

        public string CurrentMenuPathEnumString()
        {
            return CreateNameFromEnum(MenuPath1Enum);
        }
    }
}