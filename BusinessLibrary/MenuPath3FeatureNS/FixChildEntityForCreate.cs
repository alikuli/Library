using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.CounterNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Collections.Generic;

namespace UowLibrary.FeaturesNS

{
    public partial class MenuPath3FeatureBiz
    {
        public override void FixChildEntityForCreate(MenuPath3Feature mp3f)
        {
            mp3f.MenuPath3Id.IsNullOrWhiteSpaceThrowArgumentException("The MenuPath3Id is null in MenuPath3FeatureBiz.FixChildEntityForCreate");
            
            //get the parent MenuPath3
            MenuPath3 mp3 = MenuPath3Biz.Find(mp3f.MenuPath3Id);
            //Add the feauture to it.
            if(mp3.MenuPath3Features.IsNull())
            {
                mp3.MenuPath3Features = new List<MenuPath3Feature>();
            }

            mp3.MenuPath3Features.Add(mp3f);
            base.FixChildEntityForCreate(mp3f);
        }

    }
}
