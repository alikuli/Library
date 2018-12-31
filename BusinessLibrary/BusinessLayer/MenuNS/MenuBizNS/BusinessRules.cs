using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {

        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {
            MenuPathMain mpm = parm.Entity as MenuPathMain;

            base.BusinessRulesFor(parm);
            _menuPathMainBiz.BusinessRulesFor(parm);
        }

    }
}
