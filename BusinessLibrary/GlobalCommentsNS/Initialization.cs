using DatastoreNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;

namespace UowLibrary.GlobalCommentsNS
{
    public partial class GlobalCommentBiz : BusinessLayer<GlobalComment>
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
