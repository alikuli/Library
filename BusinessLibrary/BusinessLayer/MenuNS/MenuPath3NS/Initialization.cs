using DatastoreNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace UowLibrary.MenuNS

{
    public partial class MenuPath3Biz 
    {

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }


        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return MenuPath3Array.DataArray();
            }
        }




    }
}
