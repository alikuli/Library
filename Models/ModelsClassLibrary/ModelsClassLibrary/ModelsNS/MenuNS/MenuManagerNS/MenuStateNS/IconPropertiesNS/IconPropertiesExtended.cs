using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS
{
    //this has been extended so it knows if the user is the owner of the product child
    public class IconPropertiesExtended : IconProperties
    {
        public IconPropertiesExtended(
            int countIcon,
            string iconPrefix,
            string userId,
            string editFunction,
            string showFunction,
            string icon,
            string buttonColorClass,
            bool isAllowEditIfLoggedIn,
            bool isAllowedOnlyOnce,
            string badgeClassShow,
            string userPersonId,
            string ownerPersonId
            ) :
            base(
                         countIcon,
                         iconPrefix,
                         userId,
                         editFunction,
                         showFunction,
                         icon,
                         buttonColorClass,
                         isAllowEditIfLoggedIn,
                         isAllowedOnlyOnce,
                         badgeClassShow)
        {
            UserPersonId = userPersonId;
            OwnerPersonId = ownerPersonId;
        }


        string OwnerPersonId { get; set; }
        string UserPersonId { get; set; }
        /// <summary>
        /// This gets its value in _IndexMiddlePart - TiledPictures
        /// </summary>
        public bool IsProductChild { get; set; }
        public override string ButtonDisableConditionally
        {
            get
            {
                if (!IsProductChild)
                    return DISABLED;

                if (!base.ButtonDisableConditionally.IsNullOrEmpty())
                    return base.ButtonDisableConditionally;

                if (IsOwnerOfProductChild)
                    return DISABLED;

                return "";
            }
        }

        public bool IsOwnerOfProductChild
        {
            get
            {
                if (OwnerPersonId.IsNullOrWhiteSpace())
                    return false;
                if (UserPersonId.IsNullOrWhiteSpace())
                    return false;

                if (UserPersonId == OwnerPersonId)
                    return true;

                return false;
            }
        }
    }
}
