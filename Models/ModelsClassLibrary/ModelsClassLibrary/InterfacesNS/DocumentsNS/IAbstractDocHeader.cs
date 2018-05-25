using System;
using InterfacesLibrary.SharedNS;
namespace InterfacesLibrary.DocumentsNS
{
    public interface IAbstractDocHeader 
    {
        DateTime Date { get; set; }
        long DocNo { get; set; }
        //void LoadFrom(AbstractDocHeader abstractHeader);


    }
}
