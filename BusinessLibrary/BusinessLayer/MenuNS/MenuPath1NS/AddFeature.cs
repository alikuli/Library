using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.MenuNS
{
    public partial class MenuPath1Biz
    {


        //public void  AddFeature(string menuPathId, Feature feature)
        //{
        //    menuPathId.IsNullOrWhiteSpaceThrowArgumentException();
        //    if (feature.IsNull())
        //        return;

        //    //get the menuItem
        //    MenuPath1 mp1 = Find(menuPathId);
        //    mp1.IsNullThrowException();

        //    mp1.Features.Add(feature);
        //    //feature.MenuPath1 = mp1;
        //    ControllerCreateEditParameter param = new ControllerCreateEditParameter();
        //    param.Entity = mp1;
        //    UpdateAndSave(param);


        //}



        public void AddFeature(MenuFeatureModel menuFeatureModel)
        {
            //first get the parent
            menuFeatureModel.SelfCheck();
            saveFeature(menuFeatureModel);
        }

        public void DeleteFeature(MenuFeatureDeleteModel menuFeatureDeleteModel)
        {
            menuFeatureDeleteModel.SelfCheckIdsAndReturnOnly();

            MenuFeature menuFeature = MenuFeatureBiz.Find(menuFeatureDeleteModel.MenuFeatureId);
            menuFeature.IsNullThrowException("menuFeature");

            MenuPath1 menuPath1 = Find(menuFeatureDeleteModel.MenuPathId);
            menuPath1.IsNullThrowException("menuPath1");



            menuFeature.MenuPath1s.Remove(menuPath1);
            menuPath1.MenuFeatures.Remove(menuFeature);
            SaveChanges();
        }
        public void CreateNewFeature(CreateNewFeatureModel model)
        {
            model.SelfCheck();
            MenuFeature menuFeature = MenuFeatureBiz.FindByName(model.FeatureName);
            if (menuFeature.IsNull())
            {
                menuFeature = MenuFeatureBiz.Factory() as MenuFeature;
                menuFeature.IsNullThrowException("menuFeature");

                menuFeature.Name = model.FeatureName;
                MenuFeatureBiz.CreateAndSave(menuFeature);

            }
            //create the new feature.

            MenuPath1 menupath1 = Find(model.MenuPathId);
            menupath1.IsNullThrowException("menupath1");

            //taking a short cut.
            MenuFeatureModel menuFeatureModel = new MenuFeatureModel(model.MenuPathId, "", menuFeature.Id, model.ReturnUrl);
            AddFeature(menuFeatureModel);


        }
        private void saveFeature(MenuFeatureModel menuFeatureModel)
        {
            menuFeatureModel.SelfCheck();
            MenuPath1 menuPath1 = Find(menuFeatureModel.ParentId);
            menuPath1.IsNullThrowException("menuPath1");

            MenuFeature menuFeature = MenuFeatureBiz.Find(menuFeatureModel.FeatureId);
            menuFeature.IsNullThrowException("menuFeature");

            if (menuFeature.MenuPath1s.IsNull())
                menuFeature.MenuPath1s = new List<MenuPath1>();

            if (menuPath1.MenuFeatures.IsNull())
                menuPath1.MenuFeatures = new List<MenuFeature>();

            menuFeature.MenuPath1s.Add(menuPath1);
            menuPath1.MenuFeatures.Add(menuFeature);
            SaveChanges();

            addFeatureToEveryProductWithMenuPath1(menuPath1, menuFeature);






        }

        private void addFeatureToEveryProductWithMenuPath1(MenuPath1 menuPath1, MenuFeature menuFeature)
        {
            //Now add the feature to every product that has menu1 as its path.
            //first find all the menuMains that contain MenuPath1
            if (menuPath1.MenuPathMains.IsNullOrEmpty())
                return;

            List<MenuPathMain> menuPathMainList = menuPath1.MenuPathMains.ToList();
            //Now get all the products that have theseMenuPaths as their path.
            HashSet<Product> productHashList = new HashSet<Product>();

            foreach (var menuPathMain in menuPathMainList)
            {
                if (!menuPathMain.Products.IsNullOrEmpty())
                {
                    List<Product> menuPathMainProductList = menuPathMain.Products.ToList();
                    foreach (var prod in menuPathMainProductList)
                    {
                        productHashList.Add(prod);
                    }
                }
            }

            if (productHashList.IsNullOrEmpty())
                return;

            foreach (var prod2 in productHashList)
            {
                ProductFeature pf = new ProductFeature();
                pf.ProductId = prod2.Id;
                pf.Product = prod2;
                pf.MenuFeatureId = menuFeature.Id;
                pf.MenuFeature = menuFeature;
                pf.Name = menuFeature.Name;
                ProductFeatureBiz.CreateAndSave(pf);


            }
            SaveChanges();
        }




    }
}
