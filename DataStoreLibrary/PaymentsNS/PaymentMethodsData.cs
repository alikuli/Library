using System.Collections.Generic;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of PaymentMethods that the worker can speak or understand.
    /// </summary>
    public static class PaymentMethodData
    {
        public static string[] DataArray()
        {
            List<string> PaymentMethodsLst = new List<string>()
            {
                "COD",
                "Cash",
                "Coupon",
                "Cheque",
                "Bankers Draft"
            };
            return PaymentMethodsLst.ToArray();

        }
    }
}
