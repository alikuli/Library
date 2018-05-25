using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.SharedNS;
using InterfacesLibrary.SharedNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{
    public abstract class AbstractSalesmanCommission:CommonWithId
    {
        #region Salesman
        public Guid SalesmanID { get; set; }
        public virtual Salesman Salesman { get; set; } 
        #endregion


        #region LoadFrom
        public void LoadFrom(AbstractSalesmanCommission a)
        {
            base.LoadFrom(a as ICommonWithId);
            SalesmanID = a.SalesmanID;
            Salesman = a.Salesman;
        } 
        #endregion


        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            Check_SalesMan();
        }

        private void Check_SalesMan()
        {
            if (Salesman.IsNull())
            {
                throw new Exception(string.Format("Salesman is null. AbstractSalesmanCommission.Check_SalesMan"));
            }
            if (SalesmanID.IsNullOrEmpty())
            {
                throw new Exception(string.Format("SalesmanID is null. AbstractSalesmanCommission.Check_SalesMan"));
            }
        }
    }
}