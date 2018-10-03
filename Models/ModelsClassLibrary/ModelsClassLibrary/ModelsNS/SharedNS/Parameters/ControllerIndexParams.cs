using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
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
            string menuPathMainId,
            string searchFor,
            string isandForSearch,
            string selectedId,
            MenuENUM menuEnum,
            SortOrderENUM sortBy,
            string logoAddress,
            ICommonWithId entity,
            ICommonWithId dudEntity,
            string userId,
            string userName,
            bool isUserAdmin,
            bool isMenu,
            BreadCrumbManager breadCrumbManager,
            ActionNameENUM actionNameEnum,
            LikeUnlikeParameter likesCounter)
        {
            Id = id;
            SearchFor = searchFor;
            IsAndForSearch = isandForSearch == "And";
            SelectedId = selectedId;
            Entity = entity;
            SortBy = sortBy;
            Menu = new MenuParameters(menuEnum, id);
            LogoAddress = logoAddress;
            UserName = userName;
            UserId = userId;
            //User = user;
            _isUserAdmin = isUserAdmin;
            ActionNameEnum = actionNameEnum;
            DudEntity = dudEntity;
            BreadCrumbManager = breadCrumbManager;
            LikeUnlikeCounter = likesCounter; 
            IsMenu = isMenu;
            MenuPathMainId = menuPathMainId;
        }

        /// <summary>
        /// If this is true then Menu features will work in the View: _IndexMiddlePart - TiledPictures
        /// </summary>
        public bool IsMenu { get; set; }
        public string MenuPathMainId { get; set; }
        public BreadCrumbManager BreadCrumbManager { get; set; }
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
        public string UserName { get; set; }
        public string UserId { get; set; }

        //public ApplicationUser User { get; set; }

        public bool UserIsAdmin { get { return _isUserAdmin; } }
        //public string ReturnUrl { get; set; }
        public MenuParameters Menu { get; set; }
        public LikeUnlikeParameter LikeUnlikeCounter { get; set; }


    }
}
