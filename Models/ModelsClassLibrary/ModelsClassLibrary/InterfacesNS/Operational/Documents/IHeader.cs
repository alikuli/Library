using ModelsClassLibrary.Models.AddressNS;
using ModelsClassLibrary.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace ModelsClassLibrary.Interfaces
{
    public interface IHeaderOrder:ICommonWithId
    {
        DateTime DateOfDoc { get; set; }
        Customer BillTo { get; set; }
        //Address ShipTo { get; set; }
        //Address ConsignTo { get; set; }
        //Address InformTo { get; set; }
        string PoNumber { get; set; }
        DateTime ShipDateStart { get; set; }
        DateTime ShipDateEnd { get; set; }
        Salesman SalesPerson { get; set; }

        void LoadFrom(IHeaderOrder iHeaderOrder);


    }
}
