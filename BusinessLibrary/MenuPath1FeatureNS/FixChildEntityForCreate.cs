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
    public partial class MenuPath1FeatureBiz
    {
        public override void FixChildEntityForCreate(MenuPath1Feature mp1f)
        {
            mp1f.MenuPath1Id.IsNullOrWhiteSpaceThrowArgumentException("The MenuPath1Id is null in MenuPath1FeatureBiz.FixChildEntityForCreate");
            
            //get the parent MenuPath1
            MenuPath1 mp1 = MenuPath1Biz.Find(mp1f.MenuPath1Id);
            //Add the feauture to it.
            if(mp1.MenuPath1Features.IsNull())
            {
                mp1.MenuPath1Features = new List<MenuPath1Feature>();
            }

            mp1.MenuPath1Features.Add(mp1f);
            base.FixChildEntityForCreate(mp1f);
        }

    }
}
