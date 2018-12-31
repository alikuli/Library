using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Linq;


namespace UowLibrary.ProductNS
{
    /// <summary>
    /// this is tied up with JQuery.
    /// </summary>
    public partial class MenuPathMainBiz
    {



        public MenuFeature AddMenu3ItemTo_Save(string menuFeatureId, string menuPath3Id)
        {
            menuFeatureId.IsNullOrWhiteSpaceThrowArgumentException();
            menuPath3Id.IsNullOrWhiteSpaceThrowArgumentException();

            //make sure that the menuPath is not already a part of the menuFeature
            MenuFeature mf = MenuFeatureBiz.Find(menuFeatureId);
            mf.IsNullThrowException("Not found");

            //MenuPath3 mp3 = mf.MenuPath3s.FirstOrDefault(x => x.Id == menuPath3Id);

            //if (mp3.IsNull())
            //{
            //    MenuPath3 _mp3 = MenuPath3Biz.Find(menuPath3Id);
            //    _mp3.IsNullThrowException("Menu Path not found.");

            //    mf.MenuPath3s.Add(mp3);
            //    _mp3.MenuFeatures.Add(mf);
            //    MenuFeatureBiz.Update(mf);
            //}

            MenuFeatureBiz.SaveChanges();
            MenuFeatureBiz.Detach(mf);//need to do this otherwise local copy (old) is delivered.
            //freshen the data after save
            mf = MenuFeatureBiz.Find(mf.Id);
            mf.IsNullThrowException("Menu Feauture not found!");

            return mf;

        }


        public MenuFeature DeleteMenuPath3ItemFor(string menuFeatureId, string menuPath3Id)
        {
            menuFeatureId.IsNullOrWhiteSpaceThrowArgumentException();
            menuPath3Id.IsNullOrWhiteSpaceThrowArgumentException();

            //make sure that the menuPath is not already a part of the menuFeature
            MenuFeature mf = MenuFeatureBiz.Find(menuFeatureId);
            mf.IsNullThrowException("Not found");

            //MenuPath3 mp3 = mf.MenuPath3s.FirstOrDefault(x => x.Id == menuPath3Id);

            //if (!mp3.IsNull())
            //{
            //    mf.MenuPath3s.Remove(mp3);
            //    mp3.MenuFeatures.Remove(mf);
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
