using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using System.Linq;
namespace UowLibrary.MenuNS
{
    public partial class MenuPath2Biz
    {


        



        public void AddFeature(MenuFeatureModel menuFeatureModel)
        {
            //first get the parent
            menuFeatureModel.SelfCheck();

            MenuPath2 menuPath2 = Find(menuFeatureModel.ParentId);
            menuPath2.IsNullThrowException("menuPath2");

            MenuFeature menuFeature = MenuFeatureBiz.Find(menuFeatureModel.FeatureId);
            menuFeature.IsNullThrowException("menuFeature");

            if (menuFeature.MenuPath2s.IsNull())
                menuFeature.MenuPath2s = new List<MenuPath2>();

            if (menuPath2.MenuFeatures.IsNull())
                menuPath2.MenuFeatures = new List<MenuFeature>();

            menuFeature.MenuPath2s.Add(menuPath2);
            menuPath2.MenuFeatures.Add(menuFeature);
            SaveChanges();

            addFeatureToEveryProductWithMenuPath2(menuPath2, menuFeature);
        }

        public void DeleteFeature(MenuFeatureDeleteModel menuFeatureDeleteModel)
        {
            menuFeatureDeleteModel.SelfCheckIdsAndReturnOnly();

            MenuFeature menuFeature = MenuFeatureBiz.Find(menuFeatureDeleteModel.MenuFeatureId);
            menuFeature.IsNullThrowException("menuFeature");

            MenuPath2 menuPath2 = Find(menuFeatureDeleteModel.MenuPathId);
            menuPath2.IsNullThrowException("menuPath2");



            menuFeature.MenuPath2s.Remove(menuPath2);
            menuPath2.MenuFeatures.Remove(menuFeature);
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

            MenuPath2 menupath2 = Find(model.MenuPathId);
            menupath2.IsNullThrowException("menupath2");

            //taking a short cut.
            MenuFeatureModel menuFeatureModel = new MenuFeatureModel(model.MenuPathId, "", menuFeature.Id, model.ReturnUrl);
            AddFeature(menuFeatureModel);


        }


        private void addFeatureToEveryProductWithMenuPath2(MenuPath2 menuPath2, MenuFeature menuFeature)
        {
            //Now add the feature to every product that has menu1 as its path.
            //first find all the menuMains that contain MenuPath1
            if (menuPath2.MenuPathMains.IsNullOrEmpty())
                return;

            List<MenuPathMain> menuPathMainList = menuPath2.MenuPathMains.ToList();
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
