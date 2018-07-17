using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS
{
    public abstract class CreateEditMenuStateAbstract : ICreateEditMenuState
    {
        protected string _currrentViewName;
        protected string _menuPath1Id;
        protected string _menuPath2Id;
        protected string _menuPath3Id;
        protected string _productId;
        protected string _productChildId;
        protected string _menuPathMainId;
        protected string _productVmNameEditLink;

        public CreateEditMenuStateAbstract(
            string currentViewName,
            string menuPath1Id,
            string menuPath2Id,
            string menuPath3Id,
            string productId,
            string productChildId,
            string menuPathMainId,
            string productVmNameEditLink)
        {
            _currrentViewName = currentViewName;
            _menuPath1Id = menuPath1Id;
            _menuPath2Id = menuPath2Id;
            _menuPath3Id = menuPath3Id;
            _productId = productId;
            _productChildId = productChildId;
            _menuPathMainId = menuPathMainId;
            _productVmNameEditLink = productVmNameEditLink;
        }
        /// <summary>
        /// This causes the drop down list in the Edit and Create screen to disable.
        /// </summary>
        public virtual bool Disable_MenuPath1
        {
            get { return false; }
        }
        /// <summary>
        /// This causes the drop down list in the Edit and Create screen to disable.
        /// </summary>
        public virtual bool Disable_MenuPath2
        {
            get { return false; }
        }
        /// <summary>
        /// This causes the drop down list in the Edit and Create screen to disable.
        /// </summary>
        public virtual bool Disable_MenuPath3
        {
            get { return false; }
        }


        /// <summary>
        /// This is the text that the button will show.
        /// </summary>
        public virtual string CreateButtonController_Text
        {
            get { return "CREATE"; }
        }

        /// <summary>
        /// This is the controller the button will divert you to
        /// </summary>
        public virtual string CreateButtonController_ControllerName
        {
            get { return "MenuPathMains"; }
        }


        /// <summary>
        /// This is the name of the controller to which the Edit Button Directs.
        /// </summary>
        public virtual string EditLinkController_ControllerName
        {
            get { return "MenuPath1s"; }
        }


        public virtual bool ShowCreateButton
        {
            get { return false; }
        }

        public virtual string Id_EditLink
        {
            get
            {
                if (_menuPath1Id.IsNull())
                    return "";

                return _menuPath1Id;
            }
        }
        public virtual string Id_CreateLink
        {
            get
            {
                if (_menuPath2Id.IsNull())
                    return "";
                return _menuPathMainId;
            }
        }


        public string ProductVmNameEditLink
        {
            get { return _productVmNameEditLink; }
        }
    }
}
