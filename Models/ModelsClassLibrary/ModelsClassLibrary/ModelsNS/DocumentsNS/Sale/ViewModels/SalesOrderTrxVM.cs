using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{
    [NotMapped]
    public class SalesOrderTrxVM
    {
        public SalesOrderTrx NewSalesOrderTrx { get; set; }
        public List<SalesOrderTrx> SalesOrderTrxs { get; set; }
    }
}