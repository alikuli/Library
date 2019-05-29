using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;

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
        public IndexMenuVariables(string userId, string userPersonId, string productChildPersonId)
        {

            UserId = userId;
            UserPersonId = userPersonId;
            ProductChildPersonId = productChildPersonId;
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
        /// 
        //string _UserPersonId;
        //public string UserPersonId
        //{
        //    get
        //    {
        //        return _UserPersonId;
        //    }

        //    set
        //    {
        //        _UserPersonId = value;
        //    }
        //}
        #region Like

        public string UserPersonId { get; set; }


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
                    string icon = UploadedFile.GetAbsolutePath("~/ContentMine/Icons/smilingFace.png");
                    //string icon = "~/ContentMine/Icons/smilingFace.png";
                    //string icon = " far fa-smile ";
                    string buttonColorClass = " btn-info mr-1 ";
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
                string buttonColorClass = " btn-danger mr-1";
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

        public void updateRequiredProperties(string userPersonId, string productChildPersonId)
        {
            UserPersonId = userPersonId;
            ProductChildPersonId = productChildPersonId;
        }

        #region Message

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
                    string buttonColorClass = " btn-warning mr-1";
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

        #endregion


        #region Offer

        IconProperties _offerIcon;
        public IconProperties OfferIcon
        {
            get
            {
                if (_offerIcon.IsNull())
                {
                    string iconPrefix = "msg";
                    //string editFunction = "EditOfferModal(this);";
                    //string showFunction = "ShowOffersForThis(this);";
                    string editFunction = "";
                    string showFunction = "";
                    string icon = " img-offer ";
                    string buttonColorClass = " btn-success mr-1";
                    bool isAllowEditIfLoggedIn = true;
                    bool isAllowedOnlyOnce = false;
                    string badgeClassShow = " badge badge-warning ";

                    _offerIcon = new IconProperties(Count, iconPrefix, UserId, editFunction, showFunction, icon, buttonColorClass, isAllowEditIfLoggedIn, isAllowedOnlyOnce, badgeClassShow);
                }
                else
                {
                    _offerIcon.InitEachTime(Count);
                }
                return _offerIcon;
            }
        }

        #endregion

        #region Shopping Cart
        IconPropertiesExtended _shoppingCartIcon;
        public IconPropertiesExtended ShoppingCartIcon
        {
            get
            {
                if (_shoppingCartIcon.IsNull())
                {
                    string iconPrefix = "shopping";
                    string editFunction = "BuyAjax(this); ";
                    string showFunction = "";
                    string icon = "";
                    string buttonColorClass = " btn-success mr-1";
                    bool isAllowEditIfLoggedIn = true;
                    bool isAllowedOnlyOnce = false;
                    string badgeClassShow = " badge badge-warning ";
                    string ownerPersonId = ProductChildPersonId;
                    string userPersonId = UserPersonId;
                    _shoppingCartIcon = new IconPropertiesExtended(Count, iconPrefix, UserId, editFunction, showFunction, icon, buttonColorClass, isAllowEditIfLoggedIn, isAllowedOnlyOnce, badgeClassShow, userPersonId, ownerPersonId);
                }
                else
                {
                    _shoppingCartIcon.InitEachTime(Count);
                }
                return _shoppingCartIcon;
            }
        }


        #endregion

        #region Edit Child Product
        IconPropertiesExtended _iconEditProductChild;
        public IconPropertiesExtended IconEditProductChild
        {
            get
            {
                if (_iconEditProductChild.IsNull())
                {
                    string editFunction = "";
                    string showFunction = "";
                    string icon = "";
                    string iconPrefix = "EditProductChild";
                    string buttonColorClass = " btn-success mr-1";
                    bool isAllowEditIfLoggedIn = true;
                    bool isAllowedOnlyOnce = false;
                    string badgeClassShow = " badge badge-warning ";
                    string ownerPersonId = ProductChildPersonId;
                    string userPersonId = UserPersonId;

                    _iconEditProductChild = new IconPropertiesExtended(Count, iconPrefix, UserId, editFunction, showFunction, icon, buttonColorClass, isAllowEditIfLoggedIn, isAllowedOnlyOnce, badgeClassShow, userPersonId, ownerPersonId);
                }
                else
                {
                    _iconEditProductChild.InitEachTime(Count);
                }
                return _iconEditProductChild;
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

        #region Product

        public string ProductId { get; set; }
        public string ShortNameOfProduct { get; set; }


        #endregion

        #region Product Child

        public string ProductChildId { get; set; }
        public bool IsProductChild { get; set; }


        #endregion

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
