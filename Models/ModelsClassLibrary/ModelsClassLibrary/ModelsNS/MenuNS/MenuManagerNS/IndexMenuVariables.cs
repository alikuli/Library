using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;

namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS
{
    public class IndexMenuVariables
    {
        public IndexMenuVariables()
        {

        }
        public IndexMenuVariables(string userId)
        {

            UserId = userId;
        }


        public int Count { get; set; }
        public string Returnurl { get; set; }
        public string IdForEdit { get; set; }
        //public string ImagePath { get; set; }

        /// <summary>
        /// This is the owner person of the product child
        /// </summary>
        public string ProductChildPersonId { get; set; }
        
        /// <summary>
        /// this is the user person of the currently logged in user
        /// </summary>
        public string UserPersonId { get; set; }

        #region Like



        IconProperties _likeIcon;
        public IconProperties LikeIcon
        {
            get
            {
                if (_likeIcon.IsNull())
                {
                    int theCount = Count; //was 0 before I changed it.
                    string iconPrefix = "like";
                    string editFunction = "EditlikeUnlikeModal(this);";
                    string showFunction = "ShowPplWhoLikeUnlikeThis(this);";
                    string icon = " ";
                    //string icon = " far fa-smile ";
                    string buttonColorClass = " btn-info ";
                    bool isAllowEditIfLoggedIn = true;
                    bool isAllowedOnlyOnce = true;
                    string badgeClassShow = " badge badge-danger ";

                    _likeIcon = new IconProperties(theCount, iconPrefix, UserId, editFunction, showFunction, icon, buttonColorClass, isAllowEditIfLoggedIn, isAllowedOnlyOnce, badgeClassShow);

                }
                else
                {
                    _likeIcon.InitEachTime(Count);
                }
                return _likeIcon;
            }
        }

        IconProperties _unlikeIcon;
        public IconProperties UnLikeIcon
        {
            get
            {
                int theCount = Count; //was 0 before I changed it.
                string iconPrefix = "unlike";
                string editFunction = "EditlikeUnlikeModal(this);";
                string showFunction = "ShowPplWhoLikeUnlikeThis(this);";
                string icon = " ";
                //string icon = " far fa-frown ";
                string buttonColorClass = " btn-danger ";
                bool isAllowEditIfLoggedIn = true;
                bool isAllowedOnlyOnce = true;
                string badgeClassShow = " badge badge-warning ";

                if (_unlikeIcon.IsNull())
                {
                    _unlikeIcon = new IconProperties(theCount, iconPrefix, UserId, editFunction, showFunction, icon, buttonColorClass, isAllowEditIfLoggedIn, isAllowedOnlyOnce, badgeClassShow);
                }
                else
                {
                    _unlikeIcon.InitEachTime(Count);
                }

                return _unlikeIcon;
            }
            //set
            //{
            //    _unlikeIcon = value;
            //}
        }

        #endregion


        IconProperties _messageIcon;
        public IconProperties MessageIcon
        {
            get
            {
                if (_messageIcon.IsNull())
                {
                    string iconPrefix = "msg";
                    //string editFunction = "EditMessageModal(this);";
                    //string showFunction = "ShowMessagesForThis(this);";
                    string editFunction = "";
                    string showFunction = "";
                    string icon = " img-message ";
                    string buttonColorClass = " btn-warning ";
                    bool isAllowEditIfLoggedIn = true;
                    bool isAllowedOnlyOnce = false;
                    string badgeClassShow = " badge badge-warning ";

                    _messageIcon = new IconProperties(Count, iconPrefix, UserId, editFunction, showFunction, icon, buttonColorClass, isAllowEditIfLoggedIn, isAllowedOnlyOnce, badgeClassShow);
                }
                else
                {
                    _messageIcon.InitEachTime(Count);
                }
                return _messageIcon;
            }
        }


        #region Shopping Cart
        IconPropertiesShoppingCart _shoppingCartIcon;
        public IconPropertiesShoppingCart ShoppingCartIcon
        {
            get
            {
                if (_shoppingCartIcon.IsNull())
                {
                    string iconPrefix = "shopping";
                    string editFunction = "BuyAjax(this); ";
                    string showFunction = "";
                    string icon = "";
                    //string icon = " img-message ";
                    //string icon = " fas fa-envelope-square ";
                    string buttonColorClass = " btn-success ";
                    bool isAllowEditIfLoggedIn = true;
                    bool isAllowedOnlyOnce = false;
                    string badgeClassShow = " badge badge-warning ";
                    string ownerPersonId = ProductChildId;
                    _shoppingCartIcon = new IconPropertiesShoppingCart(Count, iconPrefix, UserId, editFunction, showFunction, icon, buttonColorClass, isAllowEditIfLoggedIn, isAllowedOnlyOnce, badgeClassShow, UserPersonId, ProductChildPersonId);
                }
                else
                {
                    _shoppingCartIcon.InitEachTime(Count);
                }
                return _shoppingCartIcon;
            }
        }


        #endregion

        #region Flag
        IconProperties _flagIcon;
        public IconProperties FlagIcon
        {
            get
            {
                if (_flagIcon.IsNull())
                {
                    string iconPrefix = "flag";
                    string editFunction = "";
                    string showFunction = "";
                    string icon = "";
                    string buttonColorClass = " btn-danger ";
                    bool isAllowEditIfLoggedIn = true;
                    bool isAllowedOnlyOnce = false;
                    string badgeClassShow = " badge badge-warning ";

                    _flagIcon = new IconProperties(Count, iconPrefix, UserId, editFunction, showFunction, icon, buttonColorClass, isAllowEditIfLoggedIn, isAllowedOnlyOnce, badgeClassShow);
                }
                else
                {
                    _flagIcon.InitEachTime(Count);
                }
                return _flagIcon;
            }
        }


        #endregion

        #region Hand Pointing

        public string HandPointingId
        {
            get
            {
                return "handpointingId" + Count;
            }
        }
        public string HandPointingUrl { get; set; }
        public string HandPointingCountId
        {
            get
            {
                return HandPointingId + "count";
            }
        }


        #endregion

        #region Comment

        public string CommentId
        {
            get
            {
                return "comment" + Count;
            }
        }
        public string CommentCountId
        {
            get
            {
                return CommentId + "count";
            }
        }

        public string CommentUrl { get; set; }

        #endregion

        #region MenuPaths

        public string MenuPath1Id { get; set; }
        public string MenuPath2Id { get; set; }
        public string MenuPath3Id { get; set; }
        public string MenuPathMainId { get; set; }
        public string MainMenuPath { get; set; }


        #endregion


        //#region Font Awesome Classes
        //public string ClassSmileFace
        //{
        //    get
        //    {
        //        return " far fa-smile ";
        //    }
        //}

        //public string ClassUnhappyFace
        //{
        //    get
        //    {
        //        return " far fa-frown-open ";
        //    }
        //}

        //public string ClassWrench
        //{
        //    get
        //    {
        //        return " anchorButtons btn btn-xs aria-label fas fa-wrench ";
        //    }
        //}

        //public string ClassAnchorButton
        //{
        //    get { return " anchorButtons btn btn-xs aria-label "; }
        //}

        //public string ClassAnchorButtonDisabled
        //{
        //    get { return " anchorButtons btn btn-xs aria-label disabled "; }
        //}

        //public string ClassHandPointingUp
        //{
        //    get
        //    {
        //        return " far fa-hand-point-up ";
        //    }
        //}

        //public string ClassShoppingCart
        //{
        //    get
        //    {
        //        return " fa fa-shopping-cart ";
        //    }
        //}

        //#endregion

        #region Product

        public string ProductId { get; set; }
        public string ShortNameOfProduct { get; set; }


        #endregion


        #region Product Child

        public string ProductChildId { get; set; }
        public bool IsProductChild { get; set; }


        #endregion

        //string _modalBoxId;
        //public string ModalBoxId
        //{
        //    get
        //    {
        //        return "myModal" + _modalBoxId;
        //    }
        //    set
        //    {
        //        _modalBoxId = value;
        //    }
        //}

        //public string ModalBoxIdInButton
        //{
        //    get
        //    {
        //        return "#" + ModalBoxId;
        //    }
        //}
        //public string ModalLabelId
        //{
        //    get
        //    {
        //        return "modalLabelId" + ModalBoxId;
        //    }
        //}

        #region Controller

        public string CurrController { get; set; }
        public string EditController { get; set; }


        #endregion



        #region User

        public string UserId { get; set; }
        public bool IsAdmin { get; set; }

        public bool IsLoggedIn { get { return !UserId.IsNullOrWhiteSpace(); } }

        #endregion
    }
}
