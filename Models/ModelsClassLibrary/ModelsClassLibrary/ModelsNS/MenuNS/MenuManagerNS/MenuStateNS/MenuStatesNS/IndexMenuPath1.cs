using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;

namespace UowLibrary.MenuNS.MenuStateNS.MenuStatesNS
{
    public class IndexMenuPath1 : MenuStateAbstract
    {
        public IndexMenuPath1(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum, LikeUnlikeParameters likeUnlikesCounter)
            : base(menuPathMain, product, productChild, menuEnum, likeUnlikesCounter) { }

        //public override string EditLink_Id
        //{
        //    get { throw new NotImplementedException(); }
        //}

        public override MenuENUM EditLink_MenuEnum
        {
            get { return MenuENUM.EditMenuPath1; }
        }

        public override string CreateLink_Name
        {
            get { return "Create MP1"; }
        }


        public override MenuENUM CreateLink_MenuEnum
        {
            get { return MenuENUM.CreateMenuPath1; }
        }

        public override string BackLink_Name
        {
            get { return "Back To Menu"; }
        }


        public override MenuENUM BackLink_MenuEnum
        {
            get { return MenuENUM.EditDefault; }
        }

        public override bool ShowCreateButton
        {
            get { return false; }
        }

        public override bool ShowEditButton
        {
            get { return true; }
        }

        public override string CreateAndEditLink_ControllerName
        {
            get { return "MenuPath1s"; }
        }




    }
}
