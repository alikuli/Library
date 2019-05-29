using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS
{
    public class IconProperties : ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS.IconPropertiesNS.IIconProperties
    {
        public IconProperties(
            int countIcon,
            string iconPrefix,
            string userId,
            string editFunction,
            string showFunction,
            string icon,
            string buttonColorClass,
            bool isAllowEditIfLoggedIn,
            bool isAllowedOnlyOnce,
            string badgeClassShow)
        {
            IsLoggedIn = !userId.IsNullOrWhiteSpace();
            EditFunction = editFunction;
            ShowFunction = showFunction;
            Icon = icon;
            CountIcon = countIcon;
            IconPrefix = iconPrefix;
            ButtonColorClass = buttonColorClass;
            IsAllowEditIfLoggedIn = isAllowEditIfLoggedIn;
            IsAllowedOnlyOnce = isAllowedOnlyOnce;
            BadgeClassShow = badgeClassShow;
        }

        /// <summary>
        /// This must be called each time to get correct values
        /// </summary>
        /// <param name="count"></param>
        public void InitEachTime(int count)
        {
            Count = count;
        }


        /// <summary>
        /// This is the itterator in the foreach
        /// </summary>
        private int Count { get; set; }
        private bool IsLoggedIn { get; set; }

        /// <summary>
        /// This is the javascript edit function eg: "EditlikeUnlikeModal(this);"
        /// </summary>
        private string EditFunction { get; set; }

        /// <summary>
        /// This is the javascript show function eg: "ShowPplWhoLikeUnlikeThis(this);"
        /// </summary>
        private string ShowFunction { get; set; }

        /// <summary>
        /// This is the icon. eg "fas fa-award"
        /// </summary>
        public string Icon { get; set; }


        /// <summary>
        /// This is the count for the icon
        /// </summary>
        public int CountIcon { get; set; }

        /// <summary>
        /// This is the prefix that will be used for the icon to define it's Id etc.
        /// </summary>
        public string IconPrefix { get; set; }


        /// <summary>
        /// This is the color or the button. btn-danger, btn-success etc
        /// </summary>
        public string ButtonColorClass { get; set; }


        /// <summary>
        /// This is the Action Url for the icon eg: result of 'Url.Action("Like", "LikeUnlikes", new { menuPath1Id = vv.MenuPath1Id, menuPath2Id = vv.MenuPath2Id, menuPath3Id = vv.MenuPath3Id, productId = vv.ProductId, productChildId = vv.ProductChildId, userId = vv.UserId, isLike = false });'
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// This is the Action Url for the icon eg: result of 'Url.Action("Like", "LikeUnlikes", new { menuPath1Id = vv.MenuPath1Id, menuPath2Id = vv.MenuPath2Id, menuPath3Id = vv.MenuPath3Id, productId = vv.ProductId, productChildId = vv.ProductChildId, userId = vv.UserId, isLike = false });'
        /// </summary>
        public string UrlCount { get; set; }

        /// <summary>
        /// If true then Icon, then user can only add or edit if logged
        /// </summary>
        private bool IsAllowEditIfLoggedIn { get; set; }

        /// <summary>
        /// If not allowed, and button pressed, then will show ShowFuncion, otherwise the EditFunction
        /// </summary>
        private bool IsAllowedOnlyOnce { get; set; }

        /// <summary>
        /// If pressed before, then true.
        /// </summary>
        public bool HasPressed { get; set; }




        public string Id
        {
            get
            {
                return IconPrefix + Count;
            }
        }

        public string IdCount
        {
            get
            {
                return Id + "count";
            }
        }

        //public string IconBadgeCountId
        //{
        //    get
        //    {
        //        return Id + "count";
        //    }
        //}




        public string OnClick
        {
            get
            {
                if (IsAllowEditIfLoggedIn)
                {
                    if (IsLoggedIn)
                    {
                        if (IsAllowedOnlyOnce)
                        {
                            if (HasPressed)
                            {
                                return ShowFunction;

                            }
                        }
                        return EditFunction;

                    }
                    return ShowFunction;
                }
                else
                {
                    if (IsAllowedOnlyOnce)
                    {
                        if (HasPressed)
                        {
                            return ShowFunction;
                        }
                    }
                    return EditFunction;

                }

            }
        }




        public string AnchorButtonClassWithIcon
        {
            get
            {
                string icon = " anchorButtons btn btn-sm aria-label " + ButtonColorClass + Icon;
                string disabledIcon = " disabled " + icon;

                if (IsAllowEditIfLoggedIn)
                {
                    if (IsLoggedIn)
                        return icon;
                    else
                        return disabledIcon;

                }


                return icon;
            }

        }

        public string ButtonClassWithIcon
        {
            get
            {
                string icon = " btn btn-sm aria-label " + ButtonColorClass;

                return icon;
            }

        }
        public string ButtonClass
        {
            get
            {
                string icon = " btn btn-sm aria-label" + ButtonColorClass;
                return icon;
            }

        }

        public virtual string ButtonDisableConditionally
        {
            get
            {
                //if (IsAllowEditIfLoggedIn)
                //{
                //    if (IsLoggedIn)
                //        return "";

                //}
                //return " disabled ";
                return OnlyAllowEditIfLoggedIn();

            }
        }

        protected const string DISABLED = " disabled ";
        private string OnlyAllowEditIfLoggedIn()
        {
            if (IsAllowEditIfLoggedIn)
            {
                if (IsLoggedIn)
                    return "";

            }
            return DISABLED;

        }
        private string BadgeClassHide
        {
            get { return BadgeClassShow + " d-none "; }
        }

        public string BadgeClass
        {
            get
            {
                if (CountIcon > 0)
                {
                    return BadgeClassShow;
                }

                return BadgeClassHide;
            }
        }

        /// <summary>
        /// This is the css for the badge class.
        /// </summary>
        private string BadgeClassShow { get; set; }

    }
}
