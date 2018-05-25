using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{
    public class SalesmanCommissionSalesOrder : AbstractSalesmanCommission
    {
        public void LoadFrom(SalesmanCommissionSalesOrder s)
        {
            base.LoadFrom(s as AbstractSalesmanCommission);
        }

    }
}