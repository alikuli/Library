
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;

namespace UowLibrary.MenuNS.MenuStateNS
{
    public abstract class MenuManagerAbstract : IMenuState
    {
        MenuPathMain _menuPathMain;
        Product _product;
        ProductChild _productChild;
        MenuENUM _menuEnum;
        //MenuPath1 _menuPath1;
        //MenuPath2 _menuPath2;
        //MenuPath3 _menuPath3;

        public MenuManagerAbstract(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum)
        {
            _menuPathMain = menuPathMain;
            _product = product;
            _productChild = productChild;
            _menuEnum = menuEnum;
        }

        //public abstract string EditLink_Id { get; }
        public MenuENUM MenuEnum { get { return _menuEnum; } }
        public abstract MenuENUM EditLink_MenuEnum { get; }

        public abstract string CreateLink_Name { get; }
        public abstract MenuENUM CreateLink_MenuEnum { get; }
        public abstract string CreateAndEditLink_ControllerName { get; }

        public virtual bool IsMenu
        {
            get
            {
                return true;
            }
        }
        protected MenuPathMain MenuPathMain
        {
            get
            {
                return _menuPathMain;
            }
        }
        protected Product Product
        {
            get
            {
                _product.IsNullThrowException();
                return _product;
            }
        }
        protected ProductChild ProductChild
        {
            get
            {
                _productChild.IsNullThrowException();
                return _productChild;
            }
        }
        public abstract string BackLink_Name { get; }
        public abstract MenuENUM BackLink_MenuEnum { get; }

        public abstract bool ShowCreateButton { get; }
        public abstract bool ShowEditButton { get; }

        public abstract string MenuPath1Id { get; }
        public abstract string MenuPath2Id { get; }
        public abstract string MenuPath3Id { get; }
        public virtual string ProductId
        {
            get
            {
                if (Product.IsNull())
                    return "";
                return Product.Id;
            }
        }
        public virtual string ProductChildId
        {
            get
            {
                if (ProductChild.IsNull())
                    return "";
                return ProductChild.Id;
            }
        }

        public string ControllerCurrentName { get; set; }


        public virtual string MenuPathMainId
        {
            get
            {
                if (MenuPathMain.IsNull())
                    return "";
                return MenuPathMain.Id;
            }
        }

        public MenuENUM NextMenu
        {
            get
            {
                switch (_menuEnum)
                {
                    case MenuENUM.IndexMenuPath1:
                        return MenuENUM.IndexMenuPath2;

                    case MenuENUM.IndexMenuPath2:
                        return MenuENUM.IndexMenuPath3;

                    case MenuENUM.IndexMenuPath3:
                        return MenuENUM.IndexMenuProduct;

                    case MenuENUM.IndexMenuProduct:
                        return MenuENUM.IndexMenuProductChild;

                    case MenuENUM.IndexMenuProductChild:
                        return MenuENUM.IndexMenuProductChild;

                    case MenuENUM.unknown:
                    case MenuENUM.EditMenuPath1:
                    case MenuENUM.EditMenuPath2:
                    case MenuENUM.EditMenuPath3:
                    case MenuENUM.EditMenuPathMain:
                    case MenuENUM.EditMenuProduct:
                    case MenuENUM.EditMenuProductChild:
                    case MenuENUM.CreateMenuPath1:
                    case MenuENUM.CreateMenuPath2:
                    case MenuENUM.CreateMenuPath3:
                    case MenuENUM.CreateMenuPathMenuPathMain:
                    case MenuENUM.CreateMenuProduct:
                    case MenuENUM.CreateMenuProductChild:
                    default:
                        return MenuENUM.unknown;

                }
            }
        }

        public string GetProductVM()
        {
            switch (MenuPathMain.MenuPath1.MenuPath1Enum)
            {

                case MenuPath1ENUM.Automobiles:
                    return new ProductAutomobileVM().GetType().Name + "s";
                case MenuPath1ENUM.Unknown:
                    break;
                case MenuPath1ENUM.MensClothing:
                    break;
                case MenuPath1ENUM.WomensClothing:
                    break;
                case MenuPath1ENUM.Electronics:
                    break;
                case MenuPath1ENUM.Foods:
                    break;
                case MenuPath1ENUM.HomeServants:
                    break;
                case MenuPath1ENUM.FactoryWorkers:
                    break;
                case MenuPath1ENUM.OfficeWorkers:
                    break;
                case MenuPath1ENUM.Machines:
                    break;
                case MenuPath1ENUM.Stationary:
                    break;
                case MenuPath1ENUM.FruitProccessors:
                    break;
                case MenuPath1ENUM.Steel:
                    break;
                case MenuPath1ENUM.Cement:
                    break;
                case MenuPath1ENUM.Electricity:
                    break;
                default:
                    break;
            }
            return "Products";
        }

    }
}
