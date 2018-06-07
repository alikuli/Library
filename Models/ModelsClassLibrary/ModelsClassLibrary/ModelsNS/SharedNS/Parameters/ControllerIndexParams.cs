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
        public ControllerIndexParams(string searchFor, string selectedId, SortOrderENUM sortBy, MenuLevelENUM menuLevel, string id, string menuPath1Id, string menuPath2Id, string menuPath3Id, string filepath, ICommonWithId entity, string userName, string productId)
        {
            Entity = entity;
            SearchFor = searchFor;
            SortBy = sortBy;
            SelectedId = selectedId;
            Id = id;
            Menu = new MenuParameters(menuLevel, menuPath1Id, menuPath2Id, menuPath3Id, productId);
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
