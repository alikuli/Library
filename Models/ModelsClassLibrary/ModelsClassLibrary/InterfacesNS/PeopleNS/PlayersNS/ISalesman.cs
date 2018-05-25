using System;
using InterfacesLibrary.SharedNS;
namespace InterfacesLibrary.PeopleNS.PlayersNS
{
    public interface ISalesman:IPlayer
    {
        bool IsSalesmanCategoryIdNullOrEmpty { get; }
        bool IsSalesmanCategoryNull { get; }
        void LoadFrom(ISalesman s);
        ICategory SalesmanCategory { get; set; }
        Guid? SalesmanCategoryId { get; set; }
        //void SelfErrorCheck();
    }
}
