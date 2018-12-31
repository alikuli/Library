using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.LanguageNS
{
    public partial class LanguageBiz : BusinessLayer<Language>
    {

        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            parm.Entity.Name = parm.Entity.Name.ToTitleCase();
        }

    }
}
