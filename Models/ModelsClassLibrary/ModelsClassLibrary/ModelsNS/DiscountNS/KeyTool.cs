using System;
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DiscountNS
{
    public class KeyTool
    {

        public KeyTool(Discount d, DiscountRuleENUM DiscountRule)
        {
            d.DiscountRuleEnum = DiscountRule;
            LoadKeyToolForKeyIn(d);
        }

        public DiscountRuleENUM DiscountEnum { get; set; }

        /// <summary>
        /// Could be Sale/Purchase
        /// </summary>
        public DiscountTypeENUM DiscountTypeEnum { get; set; }

        public Guid? CustomerId { get; set; }
        public Guid? CustomerCategoryId { get; set; }
        public Guid? VendorId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ProductCatMainId { get; set; }
        public Guid? TownId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? StateId { get; set; }
        public Guid? CountryId { get; set; }

        public string Key
        {
            get 
            {
                string _key = DiscountKeyMaker.MakeKey(this);
                return _key;
            }
        }
        public void SelfCheck()
        {
            if (DiscountEnum == DiscountRuleENUM.Unknown)
                throw new Exception("An unknown Discount Type received! SelfCheck.KeyToolVM");
        }


        private void LoadKeyToolForKeyIn(Discount d)
        {
            DiscountEnum = d.DiscountRuleEnum;
            DiscountTypeEnum = d.DiscountTypeEnum;
            switch (DiscountEnum)
            {
                case DiscountRuleENUM.Unknown:
                    break;

                case DiscountRuleENUM.Product:

                    ProductId = d.ProductId;
                    break;

                case DiscountRuleENUM.ProductCategory:

                    ProductCatMainId = d.ProductCategoryMainId;
                    break;

                case DiscountRuleENUM.Vendor:

                    VendorId = d.VendorId;
                    break;

                case DiscountRuleENUM.Vendor_And_Product:

                    VendorId = d.VendorId;
                    ProductId = d.ProductId;

                    break;

                case DiscountRuleENUM.Vendor_And_ProductCategory:

                    ProductCatMainId = d.ProductCategoryMainId;
                    VendorId = d.VendorId;
                    break;

                case DiscountRuleENUM.City:

                    CityId = d.CityId;
                    break;

                case DiscountRuleENUM.Country:

                    CountryId = d.CountryId;
                    break;

                case DiscountRuleENUM.Customer:

                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_City:

                    CityId = d.CityId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_State:

                    StateId = d.StateId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_Country:

                    CountryId = d.CountryId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_Product:

                    ProductId = d.ProductId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_Product_And_City:

                    CityId = d.CityId;
                    ProductId = d.ProductId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_Product_And_State:

                    StateId = d.StateId;
                    ProductId = d.ProductId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_Product_And_Country:

                    CountryId = d.CountryId;
                    ProductId = d.ProductId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_Product_And_Town:

                    TownId = d.TownId;
                    ProductId = d.ProductId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_ProductCategory:

                    ProductCatMainId = d.ProductCategoryMainId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_ProductCategory_And_City:
                    CityId = d.CityId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_ProductCategory_And_State:

                    StateId = d.StateId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_ProductCategory_And_Country:

                    CountryId = d.CountryId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_ProductCategory_And_Town:

                    TownId = d.TownId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.Customer_And_Town:

                    TownId = d.TownId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.CustomerCategory:

                    CustomerCategoryId = d.CustomerCategoryId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_Town:

                    TownId = d.TownId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_City:

                    CityId = d.CityId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    CustomerId = d.CustomerId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_State:

                    StateId = d.StateId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_Country:

                    CountryId = d.CountryId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_Product:

                    ProductId = d.ProductId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_Product_And_City:

                    CityId = d.CityId;
                    ProductId = d.ProductId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_Product_And_State:

                    StateId = d.StateId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    ProductId = d.ProductId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_Product_And_Country:

                    CountryId = d.CountryId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    ProductId = d.ProductId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_Product_And_Town:

                    TownId = d.TownId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    ProductId = d.ProductId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_ProductCategory:

                    CustomerCategoryId = d.CustomerCategoryId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_ProductCategory_And_City:

                    CityId = d.CityId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_ProductCategory_And_Country:

                    CountryId = d.CountryId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_ProductCategory_And_State:
                    StateId = d.StateId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    break;

                case DiscountRuleENUM.CustomerCategory_And_ProductCategory_And_Town:

                    TownId = d.TownId;
                    CustomerCategoryId = d.CustomerCategoryId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    break;


                case DiscountRuleENUM.Product_And_City:

                    CityId = d.CityId;
                    ProductId = d.ProductId;
                    break;

                case DiscountRuleENUM.Product_And_State:

                    StateId = d.StateId;
                    ProductId = d.ProductId;
                    break;

                case DiscountRuleENUM.Product_And_Country:

                    CountryId = d.CountryId;
                    ProductId = d.ProductId;
                    break;

                case DiscountRuleENUM.Product_And_Town:

                    TownId = d.TownId;
                    ProductId = d.ProductId;
                    break;


                case DiscountRuleENUM.ProductCategory_And_City:

                    CityId = d.CityId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    break;

                case DiscountRuleENUM.ProductCategory_And_State:

                    StateId = d.StateId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    break;

                case DiscountRuleENUM.ProductCategory_And_Country:

                    CountryId = d.CountryId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    break;

                case DiscountRuleENUM.ProductCategory_And_Town:

                    TownId = d.TownId;
                    ProductCatMainId = d.ProductCategoryMainId;
                    break;

                case DiscountRuleENUM.State:
                    StateId = d.StateId;
                    break;

                case DiscountRuleENUM.Town:
                    TownId = d.TownId;
                    break;

                default:
                    break;
            }


        }

    }
}