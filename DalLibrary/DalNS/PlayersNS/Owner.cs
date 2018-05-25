using ModelsClassLibrary.ModelsNS.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using InterfacesLibrary.PeopleNS;
using ModelsClassLibrary.ModelsNS.GeneralLedgerNS;
using InterfacesLibrary.GeneralLedgerNS;
using InterfacesLibrary.PeopleNS.PlayersNS;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    /// <summary>
    /// Owner is owner who has privilages to:
    ///     Invoice
    ///     Receive Payments against Invoice
    ///     Etc
    /// </summary>
    public class Owner : PlayerAbstract, IOwner
    {
        #region Navigation
        public ICollection<IGlAccount> GlAccounts { get; set; }
        
        #endregion

        public void LoadFrom(IOwner o)
        {
            base.LoadFrom(o as PlayerAbstract);
            GlAccounts = o.GlAccounts;
        }


    }
}