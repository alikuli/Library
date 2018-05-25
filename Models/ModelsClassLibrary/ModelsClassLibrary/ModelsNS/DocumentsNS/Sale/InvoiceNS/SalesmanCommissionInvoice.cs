using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{
    public class SalesmanCommissionInvoice : AbstractSalesmanCommission
    {
        public void LoadFrom(SalesmanCommissionInvoice s)
        {
            base.LoadFrom(s as AbstractSalesmanCommission);
        }
    }
}