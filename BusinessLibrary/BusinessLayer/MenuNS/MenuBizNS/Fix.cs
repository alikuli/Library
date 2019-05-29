using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {

        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            MenuPathMainBiz.Fix(parm);

            

        }
    }
}
