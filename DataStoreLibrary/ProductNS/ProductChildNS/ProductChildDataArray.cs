using EnumLibrary.EnumNS;
using System;
using System.Collections.Generic;
using AliKuli.Extentions;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class ProductChildtDataArray
    {
        public List<ProductChildInitializer> DataArray()
        {
            List<ProductChildInitializer> pList = new List<ProductChildInitializer>();

            string parentName = "Car";
            //ProductChildInitializer p = new ProductChildInitializer("Car -slightly used", "admin", 20000, 0, DateTime.Now.AddDays(3), "", parentName);
            //pList.Add(p);

            //ProductChildInitializer p1 = new ProductChildInitializer("Car -Not so Used", "admin", 13000, 0, DateTime.Now.AddDays(3), "1", parentName);
            //pList.Add(p1);

            //ProductChildInitializer p2 = new ProductChildInitializer("Car very used", "admin", 10000, 0, DateTime.Now.AddDays(3), "2", parentName);
            //pList.Add(p2);

            //ProductChildInitializer p3 = new ProductChildInitializer("Car yes but barely", "admin", 19000, 0, DateTime.Now.AddDays(3), "3", parentName);
            //pList.Add(p3);

            //ProductChildInitializer p4 = new ProductChildInitializer("Car is red", "admin", 18900, 0, DateTime.Now.AddDays(3), "4", parentName);
            //pList.Add(p4);

            decimal shopPrice = ModelsClassLibrary.MenuNS.MenuPathMain.Payment_To_Buy_Shop();
            parentName = "System Product";
            string adminUser = AliKuli.ConstantsNS.MyConstants.ADMIN_NAME;
            bool IsNonRefundablePaymentAccepted = true;
            ProductChildInitializer p5 = new ProductChildInitializer(ProductChildForSystemENUM.Shop.ToString().ToTitleSentance(), adminUser, shopPrice, 0, DateTime.Now.AddDays(03), "", parentName, IsNonRefundablePaymentAccepted);
            pList.Add(p5);
            return pList;
        }


    }

}
