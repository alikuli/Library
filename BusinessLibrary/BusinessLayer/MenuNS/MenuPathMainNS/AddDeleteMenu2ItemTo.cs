using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Linq;


namespace UowLibrary.ProductNS
{
    public partial class MenuPathMainBiz
    {



        public MenuFeature AddMenu2ItemTo_Save(string menuFeatureId, string menuPath2Id)
        {
            menuFeatureId.IsNullOrWhiteSpaceThrowArgumentException();
            menuPath2Id.IsNullOrWhiteSpaceThrowArgumentException();

            //make sure that the menuPath is not already a part of the menuFeature
            MenuFeature mf = MenuFeatureBiz.Find(menuFeatureId);
            mf.IsNullThrowException("Not found");

            //MenuPath2 mp2 = mf.MenuPath2s.FirstOrDefault(x => x.Id == menuPath2Id);

            //if (mp2.IsNull())
            //{
            //    MenuPath2 _mp2 = MenuPath2Biz.Find(menuPath2Id);
            //    _mp2.IsNullThrowException("Menu Path not found.");

            //    mf.MenuPath2s.Add(mp2);
            //    _mp2.MenuFeatures.Add(mf);
            //    MenuFeatureBiz.Update(mf);
            //}

            MenuFeatureBiz.SaveChanges();
            MenuFeatureBiz.Detach(mf);//need to do this otherwise local copy (old) is delivered.
            //freshen the data after save
            mf = MenuFeatureBiz.Find(mf.Id);
            mf.IsNullThrowException("Menu Feauture not found!");

            return mf;

        }


        public MenuFeature DeleteMenuPath2ItemFor(string menuFeatureId, string menuPath2Id)
        {
            menuFeatureId.IsNullOrWhiteSpaceThrowArgumentException();
            menuPath2Id.IsNullOrWhiteSpaceThrowArgumentException();

            //make sure that the menuPath is not already a part of the menuFeature
            MenuFeature mf = MenuFeatureBiz.Find(menuFeatureId);
            mf.IsNullThrowException("Not found");

            //MenuPath2 mp2 = mf.MenuPath2s.FirstOrDefault(x => x.Id == menuPath2Id);

            //if (!mp2.IsNull())
            //{
            //    mf.MenuPath2s.Remove(mp2);
            //    mp2.MenuFeatures.Remove(mf);
            //}

            MenuFeatureBiz.SaveChanges();
            MenuFeatureBiz.Detach(mf);//need to do this otherwise local copy (old) is delivered.
            //freshen the data after save
            mf = MenuFeatureBiz.Find(mf.Id);
            mf.IsNullThrowException("Menu Feauture not found!");

            return mf;


        }
    }
}
