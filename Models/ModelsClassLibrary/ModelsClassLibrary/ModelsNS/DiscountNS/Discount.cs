
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.DiscountNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.DiscountNS
{
    /// <summary>
    /// This has all the discounts stored. This record will be retrieved and if the record exits, discount will be given
    /// </summary>
    public class Discount : CommonWithId, IDiscount
    {


        #region Constructor
        public Discount()
        {
            DiscountRuleEnum = DiscountRuleENUM.Unknown;
            DiscountTypeEnum = DiscountTypeENUM.Unknown;
        }
        #endregion


        public Discount(
            DiscountTypeENUM discountTypeENUM,
            DiscountRuleENUM discountENUM,
            Customer customer, 
            CustomerCategory customerCategory,
            Vendor vendor,
            VendorCategory vendorCategory,
            Product product,
            ProductCategoryMain productCategoryMain,
            Country country,
            City city,
            Town town,
            bool isPercent = true)
        {
            DiscountTypeEnum =discountTypeENUM;
            DiscountRuleEnum= discountENUM;
            Customer =customer;
            CustomerCategory = customerCategory;
            Vendor = vendor;
            VendorCategory = vendorCategory;
            Product = product;
            ProductCategoryMain = productCategoryMain;
            Country = country;
            City = city;
            Town = town;
            IsPercent = isPercent;
        }


        string _discountKey;
        [NotMapped]
        public string DiscountKey {
            get 
            {
                KeyTool k = new KeyTool(this, DiscountRuleEnum);
                _discountKey = k.Key;
                return _discountKey;
            } 
        }


        #region Amount
        /// <summary>
        /// This is the amount of discount
        /// </summary>
        public decimal AmountDiscount { get; set; }

        #endregion


        #region DiscountType
        /// <summary>
        /// This determines the kind of discount and will also determine which field values are inputted i.e. if it is a Customer discount of CustomerCategroy 
        /// discount or a combination of the two, or vendor discount etc.
        /// </summary>
        public virtual DiscountRuleENUM DiscountRuleEnum { get; set; }

        /// <summary>
        /// Could be Sale/Purchase
        /// </summary>
        public DiscountTypeENUM DiscountTypeEnum { get; set; }

        #endregion

        #region Customer
        public Guid? CustomerId { get; set; }
        /// <summary>
        /// If this is filled, then the customer has a discount
        /// </summary>
        public virtual Customer Customer { get; set; }

        #endregion


        #region CustomerCategory
        /// <summary>
        /// If this is filled then the customer category has a discount. 
        /// </summary>
        public virtual Guid? CustomerCategoryId { get; set; }
        public virtual CustomerCategory CustomerCategory { get; set; }

        #endregion

        #region Vendor
        public Guid? VendorId { get; set; }
        /// <summary>
        /// If the vendor is filled, then the vendor has a discount
        /// </summary>
        public virtual Vendor Vendor { get; set; }

        #endregion

        public virtual Guid? VendorCategoryId { get; set; }
        public virtual VendorCategory VendorCategory { get; set; }


        #region Product
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

        #endregion


        #region ProductCategory
        public Guid? ProductCategoryMainId { get; set; }
        public virtual ProductCategoryMain ProductCategoryMain { get; set; }

        #endregion


        #region Country
        public Guid? CountryId { get; set; }
        public virtual Country Country { get; set; }

        #endregion

        #region State
        public Guid? StateId { get; set; }
        public virtual State State { get; set; }

        #endregion

        #region City
        public Guid? CityId { get; set; }
        public virtual City City { get; set; }

        #endregion

        #region Town
        public Guid? TownId { get; set; }
        public virtual Town Town { get; set; }

        #endregion


        public bool IsPercent { get; set; }




        #region SelfErrorCheck
        public override void SelfErrorCheck()
        {
            Check_DiscountKeyIsNull();
            Check_DiscountType();
            Check_Customer();
            Check_CustomerCategory();
            Check_Vendor();
            Check_VendorCategory();
            Check_Product();
            Check_State();
            Check_ProductCategory();
            Check_Country();
            Check_City();
            Check_Town();
        }

        private void Check_Town()
        {
            if (Town == null)
            {
                if (TownId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("Town is Null, whereas TownId is not. Record: '{0}'. Discount.Check_Town", ToString()));

                }
            }
            else
            {
                if (TownId.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("TownId is Null, whereas Town is not. Record: '{0}'. Discount.Check_Town", ToString()));

                }
            }
        }

        private void Check_City()
        {
            if (City == null)
            {
                if (CityId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("City is Null, whereas CityId is not. Record: '{0}'. Discount.Check_City", ToString()));

                }
            }
            else
            {
                if (CityId.IsNullOrEmpty())
                {
                    CityId = City.Id;

                }
            }

        }

        private void Check_Country()
        {
            if (Country == null)
            {
                if (CountryId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("Country is Null, whereas CountryId is not. Record: '{0}'. Discount.Check_Country", ToString()));

                }
            }
            else
            {
                if (CountryId.IsNullOrEmpty())
                {
                    CountryId = Country.Id;

                }
            }

        }

        private void Check_ProductCategory()
        {
            if (ProductCategoryMain == null)
            {
                if (ProductCategoryMainId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("ProductCategory is Null, whereas ProductCategoryId is not. Record: '{0}'. Discount.Check_ProductCategory", ToString()));

                }
            }
            else
            {
                if (ProductCategoryMainId.IsNullOrEmpty())
                {
                    ProductCategoryMainId = ProductCategoryMain.Id;

                }
            }

        }

        private void Check_State()
        {
            if (State == null)
            {
                if (StateId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("State is Null, whereas StateId is not. Record: '{0}'. Discount.Check_State", ToString()));

                }
            }
            else
            {
                if (StateId.IsNullOrEmpty())
                {
                    StateId = State.Id;

                }
            }
        }

        private void Check_Product()
        {
            if (Product == null)
            {
                if (ProductId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("Product is Null, whereas ProductId is not. Record: '{0}'. Discount.Check_Product", ToString()));

                }
            }
            else
            {
                if (ProductId.IsNullOrEmpty())
                {
                    ProductId = Product.Id;

                }
            }
        }

        private void Check_VendorCategory()
        {
            if (VendorCategory == null)
            {
                if (VendorCategoryId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("VendorCategory is Null, whereas VendorCategoryId is not. Record: '{0}'. Discount.Check_VendorCategory", ToString()));

                }
            }
            else
            {
                if (VendorCategoryId.IsNullOrEmpty())
                {
                    VendorCategoryId = VendorCategory.Id;

                }
            }
        }

        private void Check_Vendor()
        {
            if (Vendor.IsNull())
            {
                if (VendorId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("Vendor is Null, whereas VendorId is not. Record: '{0}'. Discount.Check_Vendor", ToString()));

                }
            }
            else
            {
                if (VendorId.IsNullOrEmpty())
                {
                    VendorId = Vendor.Id;

                }
            }

        }

        private void Check_CustomerCategory()
        {
            if (CustomerCategory == null)
            {
                if (CustomerCategoryId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("CustomerCategory is Null, whereas CustomerCategoryId is not. Record: '{0}'. Discount.Check_CustomerCategory", ToString()));

                }
            }
            else
            {
                if (CustomerCategoryId.IsNullOrEmpty())
                {
                    CustomerCategoryId = CustomerCategory.Id;

                }
            }

        }

        private void Check_Customer()
        {
            if (Customer == null)
            {
                if (CustomerId.IsNullOrEmpty())
                {
                    return;
                }
                else
                {
                    throw new Exception(string.Format("Customer is Null, whereas CustomerId is not. Record: '{0}'. Discount.Check_Customer", ToString()));

                }
            }
            else
            {
                if (CustomerId.IsNullOrEmpty())
                {
                    CustomerId = Customer.Id;

                }
            }
        }

        private void Check_DiscountType()
        {
            if (DiscountRuleEnum == DiscountRuleENUM.Unknown)
            {
                throw new Exception(string.Format("Discount Enum which selects the kind of discount is unknown. Record: '{0}'. Discount.Check_DiscountType", ToString()));
            }
        }

        private void Check_DiscountKeyIsNull()
        {
            if (this.DiscountKey.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException(string.Format("The Discount Key is empty. Discount. Record: '{0}'", ToString()));
        }
        #endregion

        public override string ToString()
        {


            string toString =
                string.Format("Amount: '{0}',DiscountEnum: '{1},  DiscountType: '{2}', CustomerId: '{3}', , CustomerCategoryId:: '{4}',  ProductId: '{5}', ProductCategoryId: '{6}', TownId: '{7}',  CityId:: '{8}', StateId: '{9}, CountryId:   '{10}'",
                AmountDiscount,//0
                DiscountRuleEnum.ToString(),        //1
                DiscountTypeEnum.ToString(),    //2
                CustomerId,                     //3
                CustomerCategoryId,             //4
                ProductId,                      //5
                ProductCategoryMainId,              //6
                TownId,                         //7
                CityId,                         //8
                StateId,                        //9
                CountryId                       //10
                );


            return toString;
        }


        /// <summary>
        /// This calculates the final discount amount. If it is not a purchase discount, it will return the full price
        /// </summary>
        public decimal FinalDiscountAmountForPurchase
        {
            get
            {
                
                if(DiscountTypeEnum == DiscountTypeENUM.Purchase)
                {
                    return new DiscountAmount(Product.BuyPrice, AmountDiscount, IsPercent).FinalCalculatedAmount;
                }

                return Product.BuyPrice;
            }
        }

        /// <summary>
        /// This calculates the final sale amount. If it is not a sale rule, it will return the full price.
        /// </summary>
        public decimal FinalDiscountAmountForSale
        {
            get
            {

                if (DiscountTypeEnum == DiscountTypeENUM.Sale)
                {
                    return new DiscountAmount(Product.SellPrice, AmountDiscount, IsPercent).FinalCalculatedAmount;
                }

                return Product.SellPrice;
            }
        }
    }
}
