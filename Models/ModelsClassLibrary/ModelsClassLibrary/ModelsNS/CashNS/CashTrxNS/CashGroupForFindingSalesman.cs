using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnumLibrary.EnumNS;



namespace ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS
{
    /// <summary>
    /// This is used in the algorithem to find a salesman for customer, owner and deliveryman
    /// </summary>
    public class CashGroupForFindingSalesman
    {
        public string PersonFromId { get; set; }
        public CashTypeENUM CashTypeEnum { get; set; }
        public CashStateENUM CashStateEnum { get; set; }
        public decimal TotalCash { get; set; }

    }
}
