using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using InterfacesLibrary.GeneralLedgerNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using InterfacesLibrary.SharedNS;

namespace ModelsClassLibrary.ModelsNS.GeneralLedgerNS
{
    public class GlTrx:CommonWithId, IGlTrx
    {

        public GlTrx()
        {
            DebitCreditEnum = DebitCreditENUM.Unknown;

        }
        #region Properties

        #region GlAccount
        [Display(Name = "G/L Account")]
        public long GlAccountId { get; set; }
        public IGlAccount GlAccount { get; set; }

        #endregion
        public decimal Amount { get; set; }
        #region DebitCreditEnum
        [Display(Name = "Db or Cr")]
        public DebitCreditENUM DebitCreditEnum { get; set; }

        #endregion
        #endregion
        
        public void LoadFrom(IGlTrx g)
        {
            Amount = g.Amount;
            base.LoadFrom(g as ICommonWithId);

        }

    }
}