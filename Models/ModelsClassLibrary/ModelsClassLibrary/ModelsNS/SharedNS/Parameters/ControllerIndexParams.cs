using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using System.ComponentModel.DataAnnotations.Schema;
using UserModels;

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

        readonly bool _isUserAdmin;
        public ControllerIndexParams(string searchFor, string selectedId, SortOrderENUM sortBy, MenuLevelENUM menuLevel, string id, string menuPath1Id, string menuPath2Id, string menuPath3Id, string filepath, ICommonWithId entity, ApplicationUser user, string productId, bool isUserAdmin, string returnUrl)
        {
            Entity = entity;
            SearchFor = searchFor;
            SortBy = sortBy;
            SelectedId = selectedId;
            Id = id;
            Menu = new MenuParameters(menuLevel, menuPath1Id, menuPath2Id, menuPath3Id, productId);
            LogoAddress = filepath;
            User = user;
            _isUserAdmin = isUserAdmin;
            ReturnUrl = returnUrl;
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
        public string UserName
        {
            get
            {
                if (User.IsNull())
                    return "";
                return User.UserName;
            }
        }
        public ApplicationUser User { get; set; }

        public bool UserIsAdmin { get { return _isUserAdmin; } }
        public string ReturnUrl { get; set; }

    }
}
