using ModelsClassLibrary.ModelsNS.PeopleNS;

namespace UowLibrary.LanguageNS
{
    public partial class LanguageBiz : BusinessLayer<Language>
    {

        public override string SelectListCacheKey
        {
            get { return "LanguagesSelectListData"; }
        }
    }
}
