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
        public ControllerIndexParams(
            string id,
            string searchFor,
            string isandForSearch,
            string selectedId,
            MenuENUM menuEnum,
            SortOrderENUM sortBy,
            string logoAddress,
            ICommonWithId entity,
            ICommonWithId dudEntity,
            ApplicationUser user,
            bool isUserAdmin,
            string returnUrl,
            ActionNameENUM actionNameEnum)
        {
            Id = id;
            SearchFor = searchFor;
            IsAndForSearch = isandForSearch == "And";
            SelectedId = selectedId;
            Entity = entity;
            SortBy = sortBy;
            Menu = new MenuParameters(menuEnum, id, returnUrl);
            LogoAddress = logoAddress;
            User = user;
            _isUserAdmin = isUserAdmin;
            ActionNameEnum = actionNameEnum;
            DudEntity = dudEntity;
        }
        public ActionNameENUM ActionNameEnum { get; set; }
        public bool IsAndForSearch { get; set; }
        public ICommonWithId Entity { get; set; }
        public ICommonWithId DudEntity { get; set; }
        public string SearchFor { get; set; }
        public SortOrderENUM SortBy { get; set; }
        public string SelectedId { get; set; }

        /// <summary>
        /// this is the Id of the main item
        /// </summary>
        public string Id { get; set; }
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
        //public string ReturnUrl { get; set; }
        public MenuParameters Menu { get; set; }

    }
}
