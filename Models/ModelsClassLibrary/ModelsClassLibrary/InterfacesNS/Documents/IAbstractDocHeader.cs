using ModelsClassLibrary.Interfaces;
using System;
using InterfacesLibrary.SharedNS;
namespace ModelsClassLibrary.Models.Documents.Abstract.Header
{
    public interface IAbstractDocHeader: ICommonWithId
    {
        DateTime Date { get; set; }
        long DocNo { get; set; }
        //void LoadFrom(AbstractDocHeader abstractHeader);


    }
}
