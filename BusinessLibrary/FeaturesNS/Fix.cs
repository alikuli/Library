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
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace UowLibrary.FeaturesNS

{
    public partial class FeatureBiz 
    {

        
        public override void Fix(ControllerCreateEditParameter parm)
        {
            Feature f = parm.Entity as Feature;
            f.IsNullThrowException();

            fixProductForeignKey(f);
            fixProductChildForeignKey(f);
            menuPath1ForeignKey(f);
            menuPath2ForeignKey(f);
            menuPath3ForeignKey(f);

            base.Fix(parm);

        }

        //private string fixName(string parentName, string featureName)
        //{
        //    string name = string.Format("{0}-{1}", parentName, featureName);
        //    return name;
        //}

        private void fixProductChildForeignKey(Feature f)
        {
            if (f.ProductChildId.IsNullOrWhiteSpace())
                return;

            ProductChild pc = ProductChildBiz.Find(f.ProductChildId);
            pc.IsNullThrowException();

            //f.Name = fixName(pc.Name, f.FeatureName);
            f.ProductChild = pc;
            pc.Features.Add(f);


            
        }

        private void menuPath3ForeignKey(Feature f)
        {
            if (f.MenuPath3Id.IsNullOrWhiteSpace())
                return;

            MenuPath3 mp3 = MenuPath3Biz.Find(f.MenuPath3Id);
            mp3.IsNullThrowException();

            //f.Name = fixName(mp3.Name, f.FeatureName);
            f.MenuPath3 = mp3;
            mp3.Features.Add(f);
        }

        private void menuPath2ForeignKey(Feature f)
        {
            if (f.MenuPath2Id.IsNullOrWhiteSpace())
                return;

            MenuPath2 mp2 = MenuPath2Biz.Find(f.MenuPath2Id);
            mp2.IsNullThrowException();

            //f.Name = fixName(mp2.Name, f.FeatureName);
            f.MenuPath2 = mp2;
            mp2.Features.Add(f);
        }

        private void menuPath1ForeignKey(Feature f)
        {
            if (f.MenuPath1Id.IsNullOrWhiteSpace())
                return;

            MenuPath1 mp1 = MenuPath1Biz.Find(f.MenuPath1Id);
            mp1.IsNullThrowException();

            //f.Name = fixName(mp1.Name, f.FeatureName);
            f.MenuPath1 = mp1;
            mp1.Features.Add(f);
        }

        private void fixProductForeignKey(Feature f)
        {
            if (f.ProductId.IsNullOrWhiteSpace())
                return;

            Product pc = ProductBiz.Find(f.ProductId);
            pc.IsNullThrowException();

            //f.Name = fixName(pc.Name, f.FeatureName);
            f.Product = pc;
            pc.Features.Add(f);

        }
    }
}
