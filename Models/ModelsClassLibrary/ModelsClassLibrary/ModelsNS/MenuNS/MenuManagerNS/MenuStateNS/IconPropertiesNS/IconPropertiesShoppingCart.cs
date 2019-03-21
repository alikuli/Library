using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS
{
    public class IconPropertiesShoppingCart : IconProperties
    {
        public IconPropertiesShoppingCart(
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
            string customerPersonId,
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
            CustomerPersonId = customerPersonId;
            OwnerPersonId = ownerPersonId;
        }


        public string OwnerPersonId { get; set; }
        public string CustomerPersonId { get; set; }
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
                if (CustomerPersonId.IsNullOrWhiteSpace())
                    return false;

                if (CustomerPersonId == OwnerPersonId)
                    return true;

                return false;
            }
        }
    }
}
