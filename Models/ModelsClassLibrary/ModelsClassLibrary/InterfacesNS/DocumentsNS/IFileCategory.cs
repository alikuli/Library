using System.Collections.Generic;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;

namespace InterfacesLibrary.DocumentsNS
{
    public interface IFileCategory : ICommonWithId
    {
        ICollection<IFileDoc> Files { get; set; }



        //Guid UserId { get; set; }
        //string ToString();
        //void LoadFrom(IFileCategory f);
        //int OldId { get; set; }
        //string MakeUniqueName();
        //void SelfErrorCheck();
        //string FullName();
        //string IdString();
        //void Initialize();
    }
}
