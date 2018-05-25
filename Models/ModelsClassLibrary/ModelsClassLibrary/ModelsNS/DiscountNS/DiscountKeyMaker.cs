using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using EnumLibrary.EnumNS;
using AliKuli.ConstantsNS;

namespace ModelsClassLibrary.ModelsNS.DiscountNS
{
    
    public static class DiscountKeyMaker
    {

        public static string MakeKey(KeyTool k)
        {
            DiscountRuleENUM discountType = k.DiscountEnum; 

            string customerCategoryID = (k.CustomerCategoryId.StringValueOrNull());
            string customerId = (k.CustomerId.StringValueOrNull());
            string vendorId = (k.VendorId.StringValueOrNull());
            string productId = (k.ProductId.StringValueOrNull());
            string productCatMainId = (k.ProductCatMainId.StringValueOrNull());
            string townId = (k.TownId.StringValueOrNull());
            string cityId = (k.CityId.StringValueOrNull());
            string stateId = (k.StateId.StringValueOrNull());
            string countryId = (k.CountryId.StringValueOrNull());

            return MakeKey(discountType, customerCategoryID, customerId, vendorId, productId, productCatMainId, townId, cityId, stateId, countryId);
        }


        private static string MakeKey(DiscountRuleENUM discountType, string customerCategoryID, string customerID, string vendorID, string productID, string productCatMainID, string townID, string cityID, string stateID, string countryID)
        {





            //we dont want the unknown value, we want it to be blank if its unknown.
            //Initializing all the componants. They will be blank if they dont have a value.
            string discountTypeComponant = MakeFixedLengthStringFor("");
            string customerIdComponent = MakeFixedLengthStringFor("");
            string customerCategoryComponent = MakeFixedLengthStringFor("");
            string vendorIDComponent = MakeFixedLengthStringFor("");
            string productIdComponent = MakeFixedLengthStringFor("");
            string productCatMainIdComponent = MakeFixedLengthStringFor("");
            string townIDComponent = MakeFixedLengthStringFor("");
            string cityIDComponent = MakeFixedLengthStringFor("");
            string stateIDComponent = MakeFixedLengthStringFor("");
            string countryIDComponent = MakeFixedLengthStringFor("");

            //I am converting to strings because the discount name was becoming TOO string!!
            discountTypeComponant = MakeFixedLengthStringFor(discountType.ToString());

                customerIdComponent = MakeFixedLengthStringFor(customerID.ToString());
                customerCategoryComponent = MakeFixedLengthStringFor(customerCategoryID.ToString());
                vendorIDComponent = MakeFixedLengthStringFor(vendorID.ToString());
                productIdComponent = MakeFixedLengthStringFor(productID.ToString());
                productCatMainIdComponent = MakeFixedLengthStringFor(productCatMainID.ToString());
                townIDComponent = MakeFixedLengthStringFor(townID.ToString());
                cityIDComponent = MakeFixedLengthStringFor(cityID.ToString());
                stateIDComponent = MakeFixedLengthStringFor(stateID.ToString());
                countryIDComponent = MakeFixedLengthStringFor(countryID.ToString());



            //This is the magic key!
            string theKey = discountTypeComponant + customerCategoryComponent + customerIdComponent + vendorIDComponent + productIdComponent + productCatMainIdComponent 
                + townIDComponent + cityIDComponent + stateIDComponent + countryIDComponent;
            
            
            return theKey;
        }


        //----------------------------
        
        /// <summary>
        /// this creates a fixed length string.
        /// </summary>
        /// <param name="theStringIN"></param>
        /// <returns></returns>
        private static string MakeFixedLengthStringFor(string theStringIN)
        {
            const int stringSize = MyConstants.DISCOUNT_KEY_LENGTH;
            string output = theStringIN.PadRight(stringSize);
            return output;
        }

    }
}