using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;


namespace UowLibrary.MenuNS.MenuStateNS.MenuStatesNS
{

    public class IndexMenuProductChild : MenuStateAbstract
    {

        public IndexMenuProductChild(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum, LikeUnlikeParameter likeUnlikesCounter)
            : base(menuPathMain, product, productChild, menuEnum, likeUnlikesCounter) { }

        public override MenuENUM EditLink_MenuEnum
        {
            get { return MenuENUM.EditMenuProductChild; }
        }

        public override string CreateLink_Name
        {
            get { return "Create Cust Product"; }
        }


        public override MenuENUM CreateLink_MenuEnum
        {
            get { return MenuENUM.EditMenuProductChild; }
        }

        public override string BackLink_Name
        {
            get { return "Back To Menu"; }
        }


        public override MenuENUM BackLink_MenuEnum
        {
            get { return MenuENUM.IndexMenuProduct; }
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
                return "ProductChilds";
            }
        }


        public override bool IsProductChild
        {
            get
            {
                return true;
            }
        }
        public override string MenuDisplayName
        {
            get
            {
                return string.Format("{0}", Product.FullName());
            }
        }
    }
}
