using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;


namespace UowLibrary.MenuNS.MenuStateNS.MenuStatesNS
{
    /// <summary>
    /// At this level we do not have the product. We just have the complete MenuPathMain.
    /// </summary>
    public class IndexMenuProduct : MenuManagerAbstract
    {

        public IndexMenuProduct(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum)
            : base(menuPathMain, product, productChild, menuEnum) { }


        public override MenuENUM EditLink_MenuEnum
        {
            get { return MenuENUM.EditMenuProduct; }
        }

        public override string CreateLink_Name
        {
            get { return "Create Product"; }
        }


        public override MenuENUM CreateLink_MenuEnum
        {
            get { return MenuENUM.EditMenuProduct; }
        }

        public override string BackLink_Name
        {
            get { return "Back To Menu"; }
        }


        public override MenuENUM BackLink_MenuEnum
        {
            get { return MenuENUM.IndexMenuPath3; }
        }

        public override bool ShowCreateButton
        {
            get { return true; }
        }

        public override bool ShowEditButton
        {
            get { return true; }
        }

        public override string CreateAndEditLink_ControllerName
        {
            get
            {

                return base.GetProductVM();
            }
        }

        public override string MenuPath1Id
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string MenuPath2Id
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string MenuPath3Id
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string ProductId
        {
            get
            {
                return "";
            }
        }

        public override string ProductChildId
        {
            get
            {
                return "";
            }
        }

    }
}
