using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PeopleNS;

namespace UowLibrary.LanguageNS
{
    public partial class LanguageBiz : BusinessLayer<Language>
    {

        public override void Fix(Language entity)
        {
            base.Fix(entity);
            entity.Name = entity.Name.ToTitleCase();
        }

    }
}
