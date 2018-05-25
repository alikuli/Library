using System;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;

namespace InterfacesLibrary.DocumentsNS
{
    public interface IHeaderOrder : ICommonWithId
    {
        DateTime DateOfDoc { get; set; }
        ICustomer BillTo { get; set; }
        //Address ShipTo { get; set; }
        //Address ConsignTo { get; set; }
        //Address InformTo { get; set; }
        string PoNumber { get; set; }
        DateTime ShipDateStart { get; set; }
        DateTime ShipDateEnd { get; set; }
        ISalesman SalesPerson { get; set; }

        void LoadFrom(IHeaderOrder IHeaderOrder);


    }
}
