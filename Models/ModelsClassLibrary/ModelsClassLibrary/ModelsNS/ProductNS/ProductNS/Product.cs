using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS.CheckBoxItemNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;


namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Product is in active until specifically made active.
    /// </summary>
    public partial class Product : ProductAbstract, IProduct, IHasMenuPaths
    {
        public Product()
        {
            IsUnApproved = true;
            ApprovedBy = new DateAndByComplex();
            ShopExpiryDate = new DateAndByComplex();
        }

        #region check boxes
        /// <summary>
        /// The check boxes are used for adding the menu path.
        /// Mp1List list helps to print the check baxes as collapseable buttons.
        /// </summary>

        [NotMapped]
        public List<CheckBoxItem> CheckedBoxesList { get; set; }


        [NotMapped]
        public List<CheckBoxListTree> Mp1List { get; set; }

        #endregion

        public static Product Unbox(ICommonWithId ic)
        {
            Product prod = ic as Product;
            prod.IsNullThrowException();
            return prod;
        }

        [NotMapped]
        public bool HasLiveProductChildren
        {
            get
            {
                return ProductChildren_Fixed_Not_Hidden.Count > 0;
            }
        }

        /// <summary>
        /// These get calculated in MenuBiz.GetListForIndexAsyncand & Event_ModifyIndexItem
        /// </summary>
        [NotMapped]
        public int NoOfItems { get; set; }

        [NotMapped]
        public int NoOfShops { get; set; }

        public bool IsUnApproved { get; set; }

        [Display(Name = "Approval Details")]
        public DateAndByComplex ApprovedBy { get; set; }

        public virtual ICollection<MenuPathMain> MenuPathMains { get; set; }

        [NotMapped]
        public string MainMenuIdForShop { get; set; }


        [NotMapped]
        public List<MenuPathMain> MenuPathMains_Fixed
        {
            get
            {
                if (MenuPathMains.IsNullOrEmpty())
                    return new List<MenuPathMain>();

                List<MenuPathMain> lst = MenuPathMains.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }

        public virtual ICollection<ProductChild> ProductChildren { get; set; }

        [NotMapped]
        public List<ProductChild> ProductChildren_Fixed
        {
            get
            {
                if (ProductChildren.IsNullOrEmpty())
                    return new List<ProductChild>();
                List<ProductChild> pcLst = ProductChildren.Where(x => x.MetaData.IsDeleted == false).ToList();
                return pcLst;
            }
        }
        [NotMapped]

        public List<ProductChild> ProductChildren_Fixed_Not_Hidden
        {
            get
            {
                if (ProductChildren_Fixed.IsNullOrEmpty())
                    return new List<ProductChild>();
                List<ProductChild> pcLst = ProductChildren.Where(x => x.Hide == false).ToList();
                return pcLst;
            }
        }


        ///// <summary>
        ///// This is the owner of the productShop
        ///// </summary>
        //public string ShopOwnerId { get; set; }
        //public virtual Owner ShopOwner { get; set; }

        public bool IsShop
        {
            get
            {
                return !OwnerId.IsNullOrWhiteSpace();
            }
        }
        public bool IsShopExpired
        {
            get
            {
                if (IsShop)
                {
                    DateParameter dp = new DateParameter();
                    bool shopTimeExpired = dp.Date1AfterDate2(DateTime.Now, ShopExpiryDate.Date_NotNull_Min);
                    return shopTimeExpired;
                }
                return true;
            }
        }

        public virtual ICollection<GlobalComment> GlobalComments { get; set; }

        [NotMapped]
        public List<GlobalComment> GlobalComments_Fixed
        {
            get
            {
                if (GlobalComments.IsNullOrEmpty())
                    return new List<GlobalComment>();

                List<GlobalComment> lst = GlobalComments.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }





        public virtual List<ProductFeature> ProductFeatures { get; set; }

        [NotMapped]
        public List<ProductFeature> ProductFeatures_Fixed
        {
            get
            {
                if (ProductFeatures.IsNullOrEmpty())
                    return new List<ProductFeature>();

                List<ProductFeature> lst = ProductFeatures.Where(x => x.MetaData.IsDeleted == false).OrderBy(x => x.Name).ToList();
                return lst;
            }
        }





        public virtual ICollection<Message> Messages { get; set; }


        [NotMapped]
        public List<Message> Messages_Fixed
        {
            get
            {
                if (Messages.IsNullOrEmpty())
                    return new List<Message>();

                List<Message> lst = Messages.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }



        //public virtual List<ProductSetupProblem> ProductSetupProblem { get; set; }

        [Display(Name = "Owner")]
        public string OwnerId { get; set; }

        /// <summary>
        /// This is the Owner of the shop.
        /// We will edit this directly from the menu
        /// </summary>
        public virtual Owner Owner { get; set; }

        [NotMapped]
        public SelectList SelectListOwners { get; set; }

        public DateAndByComplex ShopExpiryDate { get; set; }

        /// <summary>
        /// A product can have ONE or Many ProductIdentifiers. It must have at least one.
        /// </summary>
        public virtual ICollection<ProductIdentifier> ProductIdentifiers { get; set; }

        //stores the shop. It is a single item, but kept like this due to problems with EF
        public ICollection<BuySellDoc> BuySellDocs { get; set; }


        [NotMapped]
        public SelectList SelectListUomPurchase { get; set; }

        [NotMapped]
        public SelectList SelectListUomVolume { get; set; }

        [NotMapped]
        public SelectList SelectListUomShipWeight { get; set; }

        [NotMapped]
        public SelectList SelectListUomWeight { get; set; }

        [NotMapped]
        public SelectList SelectListUomLength { get; set; }

        [NotMapped]
        public SelectList SelectListUomDimensionsLength { get; set; }

        [NotMapped]
        public bool ShowApproveButton { get; set; }

        [NotMapped]
        public List<ProductIdentifier> ProductIdentifiers_Fixed
        {
            get
            {
                if (ProductIdentifiers.IsNullOrEmpty())
                    return new List<ProductIdentifier>();

                List<ProductIdentifier> lst = ProductIdentifiers.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }



        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.Product;
        }


    }
}