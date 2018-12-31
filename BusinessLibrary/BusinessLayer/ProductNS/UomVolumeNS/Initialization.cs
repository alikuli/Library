using AliKuli.Extentions;
using DatastoreNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Reflection;

namespace UowLibrary
{
    public partial class UomVolumeBiz : BusinessLayer<UomVolume>
    {


        #region InitializationData and InitializationDataAsync



        //public override bool Event_LockEditDuringInitialization()
        //{
        //    return false;
        //}

        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return UomVolumeArray.DataArray();
            }
        }
        #endregion

    }
}
