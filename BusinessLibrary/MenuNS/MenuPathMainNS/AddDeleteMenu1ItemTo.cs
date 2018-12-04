using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Linq;


namespace UowLibrary.ProductNS
{
    public partial class MenuPathMainBiz
    {



        public MenuFeature AddMenu1ItemTo_Save(string menuFeatureId, string menuPath1Id)
        {
            menuFeatureId.IsNullOrWhiteSpaceThrowArgumentException();
            menuPath1Id.IsNullOrWhiteSpaceThrowArgumentException();

            //make sure that the menuPath is not already a part of the menuFeature
            MenuFeature mf = MenuFeatureBiz.Find(menuFeatureId);
            mf.IsNullThrowException("Not found");

            MenuPath1 mp1 = mf.MenuPath1s.FirstOrDefault(x => x.Id == menuPath1Id);

            if (mp1.IsNull())
            {
                MenuPath1 _mp1 = MenuPath1Biz.Find(menuPath1Id);
                _mp1.IsNullThrowException("Menu Path not found.");

                mf.MenuPath1s.Add(mp1);
                _mp1.MenuFeatures.Add(mf);
                MenuFeatureBiz.Update(mf);
            }

            MenuFeatureBiz.SaveChanges();
            MenuFeatureBiz.Detach(mf);//need to do this otherwise local copy (old) is delivered.
            //freshen the data after save
            mf = MenuFeatureBiz.Find(mf.Id);
            mf.IsNullThrowException("Menu Feauture not found!");

            return mf;

        }


        public MenuFeature DeleteMenuPath1ItemFor(string menuFeatureId, string menuPath1Id)
        {
            menuFeatureId.IsNullOrWhiteSpaceThrowArgumentException();
            menuPath1Id.IsNullOrWhiteSpaceThrowArgumentException();

            //make sure that the menuPath is not already a part of the menuFeature
            MenuFeature mf = MenuFeatureBiz.Find(menuFeatureId);
            mf.IsNullThrowException("Not found");

            MenuPath1 mp1 = mf.MenuPath1s.FirstOrDefault(x => x.Id == menuPath1Id);

            if (!mp1.IsNull())
            {
                mf.MenuPath1s.Remove(mp1);
                mp1.MenuFeatures.Remove(mf);
            }

            MenuFeatureBiz.SaveChanges();
            MenuFeatureBiz.Detach(mf);//need to do this otherwise local copy (old) is delivered.
            //freshen the data after save
            mf = MenuFeatureBiz.Find(mf.Id);
            mf.IsNullThrowException("Menu Feauture not found!");

            return mf;


        }
    }
}
