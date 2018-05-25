using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using InterfacesLibrary.GeneralLedgerNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using InterfacesLibrary.PeopleNS;
using InterfacesLibrary.PeopleNS.PlayersNS;

namespace ModelsClassLibrary.ModelsNS.GeneralLedgerNS
{
    public class GlAccount:CommonWithId, IGlAccount
    {
        #region AccountNumber
        [Display(Name = "Account #")]
        [MaxLength(100)]
        public string AccountNumber { get; set; }
        
        #endregion        
        

        #region ParentAccount
        [Display(Name = "Parent Account")]
        public long? ParentAccountId { get; set; }
        public IGlAccount ParentAccount { get; set; }
        
        #endregion        

        #region Owner
        [Display(Name = "Owner")]
        public long OwnerId { get; set; }
        public IOwner Owner { get; set; }
        
        #endregion        

        public ICollection<IGlTrx> GlTrxs { get; set; }

        public void LoadFrom(IGlAccount g)
        {
            AccountNumber = g.AccountNumber;
            ParentAccount = (GlAccount)g.ParentAccount;
            ParentAccountId = g.ParentAccountId;
            Owner = g.Owner;
            GlTrxs = g.GlTrxs;//Navigation
        }

    }
}