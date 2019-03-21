using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;

namespace UowLibrary.MenuNS.MenuStateNS.MenuStatesNS
{
    public class CreateMenuProductChild : MenuStateAbstract
    {

        public CreateMenuProductChild(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum, LikeUnlikeParameters likeUnlikesCounter)
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
            get { return MenuENUM.IndexMenuProductChild; }
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
            get { return "ProductsChilds"; }
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
                if (Product.IsNull())
                    return "";

                return Product.Id;
            }
        }

        public override string ProductChildId
        {
            get { throw new System.NotImplementedException(); }
        }

    }
}
