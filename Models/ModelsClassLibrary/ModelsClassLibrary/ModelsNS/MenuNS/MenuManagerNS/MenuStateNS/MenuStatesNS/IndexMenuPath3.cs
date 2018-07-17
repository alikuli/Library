using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;


namespace UowLibrary.MenuNS.MenuStateNS.MenuStatesNS
{
    public class IndexMenuPath3 : MenuManagerAbstract
    {
        public IndexMenuPath3(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum)
            : base(menuPathMain, product, productChild, menuEnum) { }

        //public override string EditLink_Id
        //{
        //    get { throw new NotImplementedException(); }
        //}

        public override MenuENUM EditLink_MenuEnum
        {
            get { return MenuENUM.EditMenuPath3; }
        }

        public override string CreateLink_Name
        {
            get { return "Create MP3"; }
        }


        public override MenuENUM CreateLink_MenuEnum
        {
            get { return MenuENUM.CreateMenuPath3; }
        }

        public override string BackLink_Name
        {
            get { return "Back To Menu"; }
        }


        public override MenuENUM BackLink_MenuEnum
        {
            get { return MenuENUM.IndexMenuPath2; }
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
            get { return "MenuPath3s"; }
        }
        public override string MenuPath1Id
        {
            get { return MenuPathMain.MenuPath1Id; }
        }

        public override string MenuPath2Id
        {
            get { return MenuPathMain.MenuPath2Id; }
        }

        public override string MenuPath3Id
        {
            get { return MenuPathMain.MenuPath3Id; }
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
