using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.ProductFeatureNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;


namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {
        private List<string> lstOfIds = new List<string>();
        private Stack<Product> trailStack = new Stack<Product>();

        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {

            Product p = parm.Entity as Product;
            //GetDataFromMenuCheckBoxes(p);
            GetDataFromMenuPathCheckBoxes(p);
            IProduct iproduct = p as IProduct;
            addOwner(p);
            addRemoveApprover(p);
            //FixProductFeatures(iproduct);
            base.BusinessRulesFor(parm);
        }

        private void addRemoveApprover(Product p)
        {
            if (UserId.IsNullOrWhiteSpace())
                return;
            //if user is not approver dont let him do anything
            //todo
            if (p.IsUnApproved)
            {
                //remove the old info.
                p.ApprovedBy = new DateAndByComplex();
            }
            else
            {
                if (p.ApprovedBy.By.IsNullOrWhiteSpace())
                {
                    if (p.ApprovedBy.By.IsNullOrWhiteSpace())
                    {
                        p.ApprovedBy.SetToTodaysDate(UserName, UserId);
                    }
                }
            }
        }

        private void addOwner(Product p)
        {
            if (IsCreate)
            {
                if (!UserId.IsNullOrWhiteSpace())
                {
                    //user is logged in
                    //make sure the user is an Owner

                    Owner owner = OwnerBiz.GetOwnerForUser(UserId);
                    if (!owner.IsNull())
                    {
                        //user is an owner
                        //Now this user will own this product.
                        p.OwnerId = owner.Id;

                        if (owner.Products.IsNull())
                            owner.Products = new List<Product>();
                        owner.Products.Add(p);
                    }

                }
            }
        }




        public void CreateNewFeature(CreateNewFeatureModel model)
        {
            model.SelfCheck();
            ProductFeature productFeature = ProductFeatureBiz.FindByName(model.FeatureName);
            if (productFeature.IsNull())
            {
                productFeature = ProductFeatureBiz.Factory() as ProductFeature;
                productFeature.IsNullThrowException("productFeature");

                productFeature.Name = model.FeatureName;
                ProductFeatureBiz.CreateAndSave(productFeature);
            }

            //create the new feature.
            Product product = Find(model.ParentId);
            product.IsNullThrowException("product");

            //taking a short cut.
            ProductFeatureModel productFeatureModel = new ProductFeatureModel(model.ParentId, "", productFeature.Id, model.ReturnUrl, model.Description);
            AddFeature(productFeatureModel);

        }


        public void AddFeature(ProductFeatureModel productFeatureModel)
        {
            //first get the parent
            productFeatureModel.SelfCheck();
            saveFeature(productFeatureModel);
        }

        private void saveFeature(ProductFeatureModel productFeatureModel)
        {
            productFeatureModel.SelfCheck();

            Product product = Find(productFeatureModel.ParentId);
            product.IsNullThrowException("product");

            MenuFeature menuFeature = MenuFeatureBiz.Find(productFeatureModel.MenuFeatureId);
            menuFeature.IsNullThrowException("Menu feature not found.");

            //create a new product Feature and add it
            ProductFeature productFeature = ProductFeatureBiz.Factory() as ProductFeature;
            productFeature.ProductId = product.Id;
            productFeature.MenuFeatureId = menuFeature.Id;
            productFeature.Comment = productFeatureModel.Description;
            productFeature.Name = menuFeature.FullName();

            product.ProductFeatures.Add(productFeature);
            SaveChanges();


        }

        //public void DeleteFeature(ProductFeatureDeleteModel productFeatureDeleteModel)
        //{
        //    productFeatureDeleteModel.SelfCheckIdsAndReturnOnly();

        //    ProductFeature productFeature = ProductFeatureBiz.Find(productFeatureDeleteModel.ProductFeatureId);
        //    productFeature.IsNullThrowException("productFeature");

        //    Product product = Find(productFeatureDeleteModel.ProductId);
        //    product.IsNullThrowException("product");



        //    productFeature.Products.Remove(product);
        //    product.ProductFeatures.Remove(productFeature);
        //    SaveChanges();
        //}


        public void FixMenuPaths(ControllerIndexParams parm)
        {
            if (parm.MenuPathMainId.IsNullOrWhiteSpace())
                return;

            parm.Entity.IsNullThrowExceptionArgument("Entity");


            string menuPathMainId = parm.MenuPathMainId;
            Product product = parm.Entity as Product;
            product.IsNullThrowException("product");

            //get the menuPath
            MenuPathMain mpm = MenuPathMainBiz.Find(menuPathMainId);
            mpm.IsNullThrowException("mpm");

            product.MenuPathMains.Add(mpm);
        }
    }
}
