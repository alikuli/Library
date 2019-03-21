using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;

namespace UowLibrary.MenuNS.MenuStateNS.MenuStatesNS
{
    public class IndexDefault : MenuStateAbstract
    {
        public IndexDefault(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum, LikeUnlikeParameters likeUnlikesCounter)
            : base(menuPathMain, product, productChild, menuEnum, likeUnlikesCounter) { }

        //public override string EditLink_Id
        //{
        //    get { throw new NotImplementedException(); }
        //}

        public override MenuENUM EditLink_MenuEnum
        {
            get { return MenuENUM.EditDefault; }
        }

        public override string CreateLink_Name
        {
            get { return "Create"; }
        }


        public override MenuENUM CreateLink_MenuEnum
        {
            get { return MenuENUM.CreateDefault; }
        }

        public override string BackLink_Name
        {
            get { return "Back To Menu"; }
        }


        public override MenuENUM BackLink_MenuEnum
        {
            get { return MenuENUM.IndexDefault; }
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
            get { return base.ControllerCurrentName; }
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
            get { return ""; }
        }

        public override string ProductChildId
        {
            get { return ""; }
        }

        public override string MenuDisplayName
        {
            get
            {
                return "Home " + base.MenuDisplayName;
            }
        }
        public override bool IsMenu
        {
            get
            {
                return false;
            }
        }
    }
}
