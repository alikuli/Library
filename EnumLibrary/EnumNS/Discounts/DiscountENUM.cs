
namespace EnumLibrary.EnumNS
{
    /// <summary>
    /// These are the different kinds of discounts that are offered.
    /// </summary>
    public enum DiscountRuleENUM
    {

        Unknown,
        Vendor,
        Vendor_And_Product,
        Vendor_And_ProductCategory,
        City,
        Country,
        Customer,
        Customer_And_City,
        Customer_And_State,
        Customer_And_Country,
        Customer_And_Product,
        Customer_And_Product_And_City,
        Customer_And_Product_And_State,
        Customer_And_Product_And_Country,
        Customer_And_Product_And_Town,
        Customer_And_ProductCategory,
        Customer_And_ProductCategory_And_City,
        Customer_And_ProductCategory_And_State,
        Customer_And_ProductCategory_And_Country,
        Customer_And_ProductCategory_And_Town,
        Customer_And_Town,
        CustomerCategory,
        CustomerCategory_And_Town,
        CustomerCategory_And_City,
        CustomerCategory_And_State,
        CustomerCategory_And_Country,
        CustomerCategory_And_Product,
        CustomerCategory_And_Product_And_City,
        CustomerCategory_And_Product_And_State,
        CustomerCategory_And_Product_And_Country,
        CustomerCategory_And_Product_And_Town,
        CustomerCategory_And_ProductCategory,
        CustomerCategory_And_ProductCategory_And_City,
        CustomerCategory_And_ProductCategory_And_Country,
        CustomerCategory_And_ProductCategory_And_State,
        CustomerCategory_And_ProductCategory_And_Town,
        Product,
        Product_And_City,
        Product_And_State,
        Product_And_Country,
        Product_And_Town,
        ProductCategory,
        ProductCategory_And_City,
        ProductCategory_And_State,
        ProductCategory_And_Country,
        ProductCategory_And_Town,
        State,
        Town


        //Purchase_Product = 10000,
        //Purchase_ProductCategory = 10050,
        //Purchase_Vendor = 10100,
        //Purchase_Vendor_And_Product = 10150,
        //Purchase_Vendor_And_ProductCategory = 10200,

        ////Sale
        //Sale_City = 20000,
        //Sale_Country = 20050,

        //Sale_Customer = 20100,
        //Sale_Customer_And_City = 20150,
        //Sale_Customer_And_State = 20175,
        //Sale_Customer_And_Country = 20200,
        //Sale_Customer_And_Product = 20250,

        //Sale_Customer_And_Product_And_City = 20300,
        //Sale_Customer_And_Product_And_State = 20325,
        //Sale_Customer_And_Product_And_Country = 20350,
        //Sale_Customer_And_Product_And_Town = 20400,

        //Sale_Customer_And_ProductCategory = 20450,
        //Sale_Customer_And_ProductCategory_And_City = 20500,
        //Sale_Customer_And_ProductCategory_And_State = 20525,
        //Sale_Customer_And_ProductCategory_And_Country = 20550,
        //Sale_Customer_And_ProductCategory_And_Town = 20600,

        //Sale_Customer_And_Town = 20650,

        //Sale_CustomerCategory = 20700,
        //Sale_CustomerCategory_And_Town = 21350,
        //Sale_CustomerCategory_And_City = 20750,
        //Sale_CustomerCategory_And_State = 20775,
        //Sale_CustomerCategory_And_Country = 20800,
        //Sale_CustomerCategory_And_Product = 20850,

        //Sale_CustomerCategory_And_Product_And_City = 20900,
        //Sale_CustomerCategory_And_Product_And_State = 20925,
        //Sale_CustomerCategory_And_Product_And_Country = 20950,
        //Sale_CustomerCategory_And_Product_And_Town = 21000,

        //Sale_CustomerCategory_And_ProductCategory = 21050,
        //Sale_CustomerCategory_And_ProductCategory_And_City = 21100,
        //Sale_CustomerCategory_And_ProductCategory_And_Country = 21150,
        //Sale_CustomerCategory_And_ProductCategory_And_State = 21200,
        //Sale_CustomerCategory_And_ProductCategory_And_Town = 21250,


        //Sale_Product = 21400,
        //Sale_Product_And_City = 21450,
        //Sale_Product_And_State = 21475,
        //Sale_Product_And_Country = 21500,
        //Sale_Product_And_Town = 21550
        //    ,
        //Sale_ProductCategory = 21600,
        //Sale_ProductCategory_And_City = 21650,
        //Sale_ProductCategory_And_State = 21675,
        //Sale_ProductCategory_And_Country = 21700,
        //Sale_ProductCategory_And_Town = 21750,

        //SaleState = 21775,
        //SaleTown = 21800,


        //Sale_Unit_City = 30000,
        //Sale_Unit_Country = 30050,

        ////Sale_Unit_
        //Sale_Unit_Customer = 30100,
        //Sale_Unit_Customer_And_City = 30150,
        //Sale_Unit_Customer_And_State = 30175,
        //Sale_Unit_Customer_And_Country = 30200,

        //Sale_Unit_Customer_And_Product = 30250,
        //Sale_Unit_Customer_And_Product_And_City = 30300,
        //Sale_Unit_Customer_And_Product_And_State = 30325,
        //Sale_Unit_Customer_And_Product_And_Country = 30350,
        //Sale_Unit_Customer_And_Product_And_Town = 30400,
        //Sale_Unit_Customer_And_ProductCategory = 30450,

        //Sale_Unit_Customer_And_ProductCategory_And_City = 30500,
        //Sale_Unit_Customer_And_ProductCategory_And_State = 30525,
        //Sale_Unit_Customer_And_ProductCategory_And_Country = 30550,
        //Sale_Unit_Customer_And_ProductCategory_And_Town = 30600,
        //Sale_Unit_Customer_And_Town = 30650,

        //Sale_Unit_CustomerCategory = 30700,
        //Sale_Unit_CustomerCategory_And_City = 30750,
        //Sale_Unit_CustomerCategory_And_State = 30775,
        //Sale_Unit_CustomerCategory_And_Country = 30800,

        //Sale_Unit_CustomerCategory_And_Product = 30850,
        //Sale_Unit_CustomerCategory_And_Product_And_City = 30900,
        //Sale_Unit_CustomerCategory_And_Product_And_State = 30925,
        //Sale_Unit_CustomerCategory_And_Product_And_Country = 30950,
        //Sale_Unit_CustomerCategory_And_Product_And_Town = 31000,

        //Sale_Unit_CustomerCategory_And_ProductCategory = 31050,
        //Sale_Unit_CustomerCategory_And_ProductCategory_And_City = 31100,
        //Sale_Unit_CustomerCategory_And_ProductCategory_And_State = 31125,
        //Sale_Unit_CustomerCategory_And_ProductCategory_And_Country = 31150,
        //Sale_Unit_CustomerCategory_And_ProductCategory_And_Town = 31200,
        //Sale_Unit_CustomerCategory_And_Town = 31250,

        //Sale_Unit_Product = 31300,
        //Sale_Unit_Product_And_City = 31350,
        //Sale_Unit_Product_And_State = 31375,
        //Sale_Unit_Product_And_Country = 31400,
        //Sale_Unit_Product_And_Town = 31450,

        //Sale_Unit_ProductCategory = 31500,
        //Sale_Unit_ProductCategory_And_City = 31550,
        //Sale_Unit_ProductCategory_And_State = 31575,
        //Sale_Unit_ProductCategory_And_Country = 31600,
        //Sale_Unit_ProductCategory_And_Town = 31650,

        //Sale_Unit_State = 31675,
        //Sale_Unit_Town = 31700,



    }

    //public static class DiscountNames
    //{
    //    public static string GetName(DiscountENUM d)
    //    {
    //        return (d.ToString()).ToSentence();
    //    }

    //    public static string ConvertDiscountEnumToString(DiscountENUM d)
    //    {
    //        return d.ToString();
    //    }

    //    /// <summary>
    //    /// This converts the long value to DiscountENUM. If it is not a DiscountENUM, an Excetion is thrown
    //    /// </summary>
    //    /// <param name="num"></param>
    //    /// <returns></returns>
    //    public static DiscountENUM ConvertLongToDiscountEnum(long num)
    //    {
    //        try
    //        {
    //            if (IsDiscountName(num))
    //            {
    //                DiscountENUM theEnum = (DiscountENUM)num;
    //                return theEnum;

    //            }

    //            else
    //                throw new Exception("Enum from Long to DiscountENUM failed. (ConvertLongToDiscountEnum)");

    //        }
    //        catch
    //        {
    //            throw;
    //        }
    //    }

    //    public static bool IsDiscountName(long incomming)
    //    {
    //        //Converting the incoming Enum
    //        DiscountENUM incommingEnum = (DiscountENUM)incomming;

    //        //Checking the conversion
    //        foreach (DiscountENUM item in Enum.GetValues(typeof(DiscountENUM)))
    //        {
    //            //Convert item back to the enum we want
    //            DiscountENUM testEnum = (DiscountENUM)Enum.Parse(typeof(DiscountENUM), item.ToString());
    //            if (incommingEnum.HasFlag(testEnum))
    //            {
    //                return true;
    //            }

    //        }

    //        return false;
    //    }
}
