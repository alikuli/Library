using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace UowLibrary.MenuNS.MenuStateNS.MenuStatesNS
{
    public class IndexMenuPath1 : MenuManagerAbstract
    {
        public IndexMenuPath1(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum)
            : base(menuPathMain, product, productChild, menuEnum) { }

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
            get { return MenuENUM.unknown; }
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

        public override string MenuPath1Id
        {
            get
            {
                MenuPathMain.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
                return MenuPathMain.MenuPath1Id;
            }
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

    }
}
