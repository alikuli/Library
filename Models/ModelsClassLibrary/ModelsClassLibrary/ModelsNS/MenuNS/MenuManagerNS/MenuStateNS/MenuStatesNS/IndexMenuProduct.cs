using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;

namespace UowLibrary.MenuNS.MenuStateNS.MenuStatesNS
{
    /// <summary>
    /// At this level we do not have the product. We just have the complete MenuPathMain.
    /// </summary>
    public class IndexMenuProduct : MenuStateAbstract
    {

        public IndexMenuProduct(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum, LikeUnlikeParameters likeUnlikesCounter, string userId, string userName)
            : base(menuPathMain, product, productChild, menuEnum, likeUnlikesCounter, userId, userName) { }



        public override MenuENUM EditLink_MenuEnum
        {
            get { return MenuENUM.EditMenuProduct; }
        }

        public override string CreateLink_Name
        {
            get { return "Create Shop"; }
        }


        public override MenuENUM CreateLink_MenuEnum
        {
            get { return MenuENUM.EditMenuProduct; }
        }

        public override string BackLink_Name
        {
            get { return "Back To Menu"; }
        }

        public override string CreateButtonAction
        {
            get
            {
                return "CreateShop";
            }
        }

        public override bool ShowCreateProductButton { get { return true; } }


        public override MenuENUM BackLink_MenuEnum
        {
            get { return MenuENUM.IndexMenuPath3; }
        }

        public override bool ShowCreateButton
        {
            get
            {
                if (UserId.IsNull())
                    return false;
                return true;
            }
        }

        public override bool ShowEditButton
        {
            get { return true; }
        }

        public override string CreateAndEditLink_ControllerName
        {
            get
            {

                return "Shops";
            }
        }




        public override string MenuDisplayName
        {
            get
            {
                return string.Format("{0}", MenuPathMain.MenuPath3.FullName());
            }
        }


    }
}
