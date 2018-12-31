using DatastoreNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;

namespace UowLibrary.LanguageNS
{
    public partial class LanguageBiz : BusinessLayer<Language>
    {

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }
        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return LanguagesData.DataArray();
            }
        }


    }
}
