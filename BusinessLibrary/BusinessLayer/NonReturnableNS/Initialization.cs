using DatastoreNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;

namespace UowLibrary.NonReturnableNS
{
    public partial class NonReturnableTrxBiz
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
