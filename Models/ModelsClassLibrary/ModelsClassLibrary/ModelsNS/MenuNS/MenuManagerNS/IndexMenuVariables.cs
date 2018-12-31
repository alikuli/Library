using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using System.Collections.Generic;

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





        #region Like



        IconProperties _likeIcon;
        public IconProperties LikeIcon
        {
            get
            {
                if (_likeIcon.IsNull())
                {
                    _likeIcon = new IconProperties(Count, "like", UserId, "EditlikeUnlikeModal(this);", "ShowPplWhoLikeUnlikeThis(this);", " far fa-smile ", " btn-info ", true, true, " badge badge-danger ");

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

                if (_unlikeIcon.IsNull())
                {
                    _unlikeIcon = new IconProperties(0, "unlike", UserId, "EditlikeUnlikeModal(this);", "ShowPplWhoLikeUnlikeThis(this);", " far fa-frown ", " btn-danger ", true, true, " badge badge-warning ");
                }
                else
                {
                    _unlikeIcon.InitEachTime(Count);
                }

                return _unlikeIcon;
            }
            set
            {
                _unlikeIcon = value;
            }
        }


        #endregion





        #region Flag
        public string FlagId
        {
            get
            {
                return "flag" + Count;
            }
        }
        public string FlagUrl { get; set; }
        public string FlagOnClick
        {
            get
            {
                if (IsLoggedIn)
                    if (!HasFlagd)
                        return "EditlikeFlagModal(this);";

                return "ShowPplWhoLikeFlagThis(this);";
            }
        }
        public string FlagCountUrl { get; set; }

        public string FlagAwesomeFont
        {
            get
            {
                return " far fa-flag ";
            }
        }
        public string FlagClass
        {
            get
            {
                string flag = " anchorButtons btn btn-xs btn-danger aria-label " + FlagAwesomeFont;
                string disabledflag = " disabled " + flag;

                string flagclass = flag;
                if (UserId.IsNullOrWhiteSpace())
                {
                    flagclass = disabledflag;
                }

                return flagclass;
            }

        }
        public string FlagCountId
        {
            get
            {
                return FlagId + "count";
            }
        }
        public bool HasFlagd { get; set; }

        public string FlagBadgeCountId
        {
            get
            {
                return FlagId + "countspan";
            }
        }

        private string FlagBadgeCss
        {
            get { return " badge badge-warning "; }
        }
        private string FlagBadgeCssHide
        {
            get { return " badge badge-warning d-none "; }
        }

        public int FlagCount { get; set; }
        public string FlagBadgeClass
        {
            get
            {
                if (FlagCount > 0)
                {
                    return FlagBadgeCss;
                }

                return FlagBadgeCssHide;
            }
        }



        #endregion


        #region Shopping Cart
        public string ShoppingCartId { get; set; }

        public string ShoppingCartUrl { get; set; }
        public string ShoppingCartCountId { get; set; }


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


        #region Font Awesome Classes
        public string ClassSmileFace
        {
            get
            {
                return " far fa-smile ";
            }
        }

        public string ClassUnhappyFace
        {
            get
            {
                return " far fa-frown-open ";
            }
        }

        public string ClassWrench
        {
            get
            {
                return " anchorButtons btn btn-xs aria-label fas fa-wrench ";
            }
        }

        public string ClassAnchorButton
        {
            get { return " anchorButtons btn btn-xs aria-label "; }
        }

        public string ClassAnchorButtonDisabled
        {
            get { return " anchorButtons btn btn-xs aria-label disabled "; }
        }

        public string ClassHandPointingUp
        {
            get
            {
                return " far fa-hand-point-up ";
            }
        }

        public string ClassShoppingCart
        {
            get
            {
                return " fa fa-shopping-cart ";
            }
        }

        #endregion

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
