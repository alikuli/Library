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
    public partial class MenuPath2FeatureBiz
    {
        public override void FixChildEntityForCreate(MenuPath2Feature mp2f)
        {
            mp2f.MenuPath2Id.IsNullOrWhiteSpaceThrowArgumentException("The MenuPath2Id is null in MenuPath2FeatureBiz.FixChildEntityForCreate");
            
            //get the parent MenuPath2
            MenuPath2 mp2 = MenuPath2Biz.Find(mp2f.MenuPath2Id);
            //Add the feauture to it.
            if(mp2.MenuPath2Features.IsNull())
            {
                mp2.MenuPath2Features = new List<MenuPath2Feature>();
            }

            mp2.MenuPath2Features.Add(mp2f);
            base.FixChildEntityForCreate(mp2f);
        }

    }
}
