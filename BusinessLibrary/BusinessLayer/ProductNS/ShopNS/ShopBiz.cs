using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Threading.Tasks;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.ProductNS;

namespace UowLibrary.BusinessLayer.ProductNS.ShopNS
{
    public class ShopBiz : ProductBiz
    {
        ProductBiz _productBiz;
        public ShopBiz(ProductBiz productBiz, IRepositry<Product> entityDal) :

            base(productBiz.UserBiz, entityDal, productBiz.MyWorkClassesProduct, productBiz.ProductChildBiz, productBiz.BizParameters, productBiz.LikeUnlikeBiz, productBiz.OwnerBiz)
        {
            _productBiz = productBiz;
        }

        public ProductBiz ProductBiz
        {
            get
            {
                _productBiz.UserId = UserId;
                _productBiz.UserName = UserName;
                return _productBiz;
            }
        }



        public override void CreateAndSave(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            Product product = Product.Unbox(parm.Entity);
            if (UserId.IsNullOrWhiteSpace())
                return;

            if (product.OwnerId.IsNullOrWhiteSpace())
            {
                Owner owner = OwnerBiz.GetPlayerFor(UserId);
                owner.IsNullThrowException("You must first become a seller. Go to ' I Want To...' to become a seller.");

                product.OwnerId = owner.Id;
                if (owner.Shops.IsNull())
                    owner.Shops = new List<Product>();
                owner.Shops.Add(product);
            }

            base.CreateAndSave(parm);
        }


        public override async Task CreateAndSaveAsync(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            Product product = Product.Unbox(parm.Entity);
            if (UserId.IsNullOrWhiteSpace())
                return;

            await base.CreateAndSaveAsync(parm);
        }

        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {


            if (IsCreate)
            {
                Product product = Product.Unbox(parm.Entity);
                product.IsUnApproved = false;

                Owner owner = OwnerBiz.GetPlayerFor(UserId);
                owner.IsNullThrowException("You must first become a seller. Go to ' I Want To...' to become a seller.");

                product.OwnerId = owner.Id;

                if (owner.Shops.IsNull())
                    owner.Shops = new List<Product>();

                owner.Shops.Add(product);


                product.MainMenuIdForShop.IsNullOrWhiteSpaceThrowException();
                MenuPathMain mpm = MenuPathMainBiz.Find(product.MainMenuIdForShop);

                mpm.IsNullThrowException();
                if (mpm.Products.IsNull())
                    mpm.Products = new List<Product>();
                if (product.MenuPathMains.IsNull())
                    product.MenuPathMains = new List<MenuPathMain>();
                mpm.Products.Add(product);
                product.MenuPathMains.Add(mpm);
            }

            base.BusinessRulesFor(parm);
        }
    }


}
