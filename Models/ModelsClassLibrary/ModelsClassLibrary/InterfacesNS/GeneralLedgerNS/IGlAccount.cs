using System;
using System.Collections.Generic;
using InterfacesLibrary.PeopleNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
namespace InterfacesLibrary.GeneralLedgerNS
{
    public interface IGlAccount : ICommonWithId
    {
        string AccountNumber { get; set; }
        ICollection<IGlTrx> GlTrxs { get; set; }
        void LoadFrom(IGlAccount g);
        IOwner Owner { get; set; }
        long OwnerId { get; set; }
        IGlAccount ParentAccount { get; set; }
        long? ParentAccountId { get; set; }
    }
}
