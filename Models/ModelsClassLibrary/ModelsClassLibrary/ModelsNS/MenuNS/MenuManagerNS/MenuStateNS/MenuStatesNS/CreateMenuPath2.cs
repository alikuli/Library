using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace UowLibrary.MenuNS.MenuStateNS.MenuStatesNS
{
    public class CreateMenuPath2 : MenuManagerAbstract
    {

        public CreateMenuPath2(MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuENUM menuEnum)
            : base(menuPathMain, product, productChild, menuEnum)
        {

        }

        public override MenuENUM EditLink_MenuEnum
        {
            get { return MenuENUM.EditMenuPath2; }
        }

        public override string CreateLink_Name
        {
            get { return "Create MP2"; }
        }


        public override MenuENUM CreateLink_MenuEnum
        {
            get { return MenuENUM.CreateMenuPath2; }
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
            get { return "MenuPath2s"; }

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
            get { throw new System.NotImplementedException(); }
        }

        public override string ProductChildId
        {
            get { throw new System.NotImplementedException(); }
        }

    }
}
