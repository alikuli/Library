using System;
using System.Collections.Generic;
using EnumLibrary.EnumNS;
using AliKuli.Extentions;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of PaymentMethods that the worker can speak or understand.
    /// </summary>
    public class PaymentTermsData
    {
        public static string[] DataArray()
        {
            string[] paymentTermsArray = Enum.GetNames(typeof(PaymentTermsENUM));

            if (paymentTermsArray.IsNullOrEmpty())
                return null;

            for (int i = 0; i < paymentTermsArray.Length; i++)
            {
                string s = paymentTermsArray[i];
                string newS = s.ToTitleSentance();
                paymentTermsArray[i] =  newS;
            }
            return paymentTermsArray;

        }
    }
}
