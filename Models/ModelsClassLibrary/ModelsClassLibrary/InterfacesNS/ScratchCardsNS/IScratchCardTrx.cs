using System;
using EnumLibrary.EnumNS;
using InterfacesLibrary.ProductNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
namespace InterfacesLibrary.ProductNS
{
    public interface IScratchCardTrx
    {
        DebitCreditENUM DebitOrCreditEnum { get; set; }
        string FullName();
        IScratchCard ScratchCard { get; set; }
        Guid ScratchCardID { get; set; }
        void SelfErrorCheck();
        int UnitsForTrx { get; set; }
        //IUserCustom UsedByUser { get; set; }
        Guid UsedByUserId { get; set; }
    }
}
