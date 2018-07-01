using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {

        public override void BusinessRulesFor(MenuPathMain entity)
        {
            base.BusinessRulesFor(entity);
            _menuPathMainBiz.BusinessRulesFor(entity);
        }

    }
}
