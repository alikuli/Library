using System;

namespace UowLibrary.PaymentTypeNS
{
    public partial class PaymentTypeBiz
    {


        #region InitializationData and InitializationDataAsync
        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                throw new NotImplementedException();
                //return PaymentTypeData.DataArray();
            }
        }

        #endregion
    }
}
