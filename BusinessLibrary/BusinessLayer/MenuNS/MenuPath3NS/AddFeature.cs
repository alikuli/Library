using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using UowLibrary.FeatureNS.MenuFeatureNS;
using System.Linq;

namespace UowLibrary.MenuNS
{
    public partial class MenuPath3Biz
    {


        



        public void AddFeature(MenuFeatureModel menuFeatureModel)
        {
            //first get the parent
            menuFeatureModel.SelfCheck();

            MenuPath3 menuPath3 = Find(menuFeatureModel.ParentId);
            menuPath3.IsNullThrowException("menuPath3");

            MenuFeature menuFeature = MenuFeatureBiz.Find(menuFeatureModel.FeatureId);
            menuFeature.IsNullThrowException("menuFeature");

            if (menuFeature.MenuPath3s.IsNull())
                menuFeature.MenuPath3s = new List<MenuPath3>();

            if (menuPath3.MenuFeatures.IsNull())
                menuPath3.MenuFeatures = new List<MenuFeature>();

            menuFeature.MenuPath3s.Add(menuPath3);
            menuPath3.MenuFeatures.Add(menuFeature);
            SaveChanges();
        }

        public void DeleteFeature(MenuFeatureDeleteModel menuFeatureDeleteModel)
        {
            menuFeatureDeleteModel.SelfCheckIdsAndReturnOnly();

            MenuFeature menuFeature = MenuFeatureBiz.Find(menuFeatureDeleteModel.MenuFeatureId);
            menuFeature.IsNullThrowException("menuFeature");

            MenuPath3 menuPath3 = Find(menuFeatureDeleteModel.MenuPathId);
            menuPath3.IsNullThrowException("menuPath3");



            menuFeature.MenuPath3s.Remove(menuPath3);
            menuPath3.MenuFeatures.Remove(menuFeature);
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

            MenuPath3 menupath3 = Find(model.MenuPathId);
            menupath3.IsNullThrowException("menupath3");

            //taking a short cut.
            MenuFeatureModel menuFeatureModel = new MenuFeatureModel(model.MenuPathId, "", menuFeature.Id, model.ReturnUrl);
            AddFeature(menuFeatureModel);


        }


        private void addFeatureToEveryProductWithMenuPath3(MenuPath3 menuPath3, MenuFeature menuFeature)
        {
            //Now add the feature to every product that has menu3 as its path.
            //first find all the menuMains that contain MenuPath3
            if (menuPath3.MenuPathMains.IsNullOrEmpty())
                return;

            List<MenuPathMain> menuPathMainList = menuPath3.MenuPathMains.ToList();
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
