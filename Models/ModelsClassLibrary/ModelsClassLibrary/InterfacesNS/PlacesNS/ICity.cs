using System;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace InterfacesLibrary.PlacesNS
{
    public interface ICity : ICommonWithId
    {
        IState State { get; set; }
        string StateId { get; set; }
        string Abbreviation { get; set; }


    }
}
