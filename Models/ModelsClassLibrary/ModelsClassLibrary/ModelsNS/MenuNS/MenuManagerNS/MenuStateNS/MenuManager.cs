using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary.MenuNS.MenuStateNS.MenuStatesNS;

namespace UowLibrary.MenuNS.MenuStateNS
{
    public class MenuManager : IMenuManager
    {
        public MenuManager()
        {
        }
        public MenuManager(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum)
            : this()
        {
            MenuPathMain = menuPathMain;
            Product = product;
            ProductChild = productChild;
            MenuEnum = menuEnum;
            LoadMenuState();
        }

        private void LoadMenuState()
        {
            switch (MenuEnum)
            {
                case MenuENUM.unknown:
                    break;

                case MenuENUM.IndexMenuPath1:
                    MenuState = new IndexMenuPath1(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.IndexMenuPath2:
                    MenuState = new IndexMenuPath2(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.IndexMenuPath3:
                    MenuState = new IndexMenuPath3(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.IndexMenuProduct:
                    MenuState = new IndexMenuProduct(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.IndexMenuProductChild:
                    MenuState = new IndexMenuProductChild(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.EditMenuPath1:
                    MenuState = new EditMenuPath1(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.EditMenuPath2:
                    MenuState = new EditMenuPath2(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.EditMenuPath3:
                    MenuState = new EditMenuPath3(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.EditMenuProduct:
                    MenuState = new EditMenuProduct(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.EditMenuProductChild:
                    MenuState = new EditMenuProductChild(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;


                case MenuENUM.CreateMenuPath1:
                    MenuState = new CreateMenuPath1(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.CreateMenuPath2:
                    MenuState = new CreateMenuPath2(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.CreateMenuPath3:
                    MenuState = new CreateMenuPath3(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.CreateMenuProduct:
                    MenuState = new CreateMenuProduct(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.CreateMenuProductChild:
                    MenuState = new CreateMenuProductChild(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;

                case MenuENUM.CreateDefault:
                    MenuState = new CreateDefault(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;
                case MenuENUM.IndexDefault:
                    MenuState = new IndexDefault(MenuPathMain, Product, ProductChild, MenuEnum);
                    break;

                default:
                    break;
            }
        }

        IMenuState _menuState;
        public IMenuState MenuState
        {
            get
            {
                return _menuState;
            }
            set
            {
                _menuState = value;
                _menuState.IsNullThrowException();

            }
        }


        private MenuENUM MenuEnum { get; set; }
        public MenuENUM MenuEnumCreateButton { get; set; }
        public MenuENUM MenuEnumEditButtom { get; set; }


        public MenuPathMain MenuPathMain { get; set; }
        public Product Product { get; set; }
        public ProductChild ProductChild { get; set; }


        //public string MenuPath1Id { get; set; }
        //public string MenuPath2Id { get; set; }
        //public string MenuPath3Id { get; set; }

        public BreadCrumbManager BreadCrumbManager { get; set; }
        public string CurrentUrl { get; set; }
        public string SelectedId { get; set; }
        public string SearchString { get; set; }
        public SortOrderENUM SortOrderEnum { get; set; }





    }
}
