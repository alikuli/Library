using DatastoreNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.OwnerCategoryNS
{
    public partial class OwnerCategoryBiz : BusinessLayer<OwnerCategory>
    {


        #region InitializationData and InitializationDataAsync

        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return OwnerCategoryData.DataArray();
            }
        }


        #endregion



    }
}
