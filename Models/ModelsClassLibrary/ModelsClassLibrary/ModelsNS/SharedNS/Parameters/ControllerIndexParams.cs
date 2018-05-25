using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    /// <summary>
    /// These are the parameters that are returned by all the index methods. This class is used just to transport them.
    /// Now, everytime you change the parameters, this class will shield the change from all the various layers.
    /// </summary>
    [NotMapped]
    public class ControllerIndexParams
    {
        public ControllerIndexParams()
        {

        }
        public ControllerIndexParams(string searchFor, string selectedId, SortOrderENUM sortBy, MenuLevelENUM menuLevel, string id, string productCat1Id, string productCat2Id, string productCat3Id, string filepath, ICommonWithId entity, string userName)
        {
            Entity = entity;
            SearchFor = searchFor;
            SortBy = sortBy;
            SelectedId = selectedId;
            Id = id;
            Menu = new MenuParameters(menuLevel, productCat1Id, productCat2Id, productCat3Id);
            LogoAddress = filepath;
            UserName = userName;
        }
        public ICommonWithId Entity { get; set; }
        public ICommonWithId DudEntity { get; set; }
        public string SearchFor { get; set; }
        public SortOrderENUM SortBy { get; set; }
        public string SelectedId { get; set; }

        /// <summary>
        /// this is the Id of the main item
        /// </summary>
        public string Id { get; set; }
        public MenuParameters Menu { get; set; }
        public string LogoAddress { get; set; }
        public string UserName { get; set; }

    }
}
