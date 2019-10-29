using AliKuli.Extentions;
using DatastoreNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using System;
using System.Collections.Generic;
using System.Reflection;
using UserModels;
using System.Linq;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz
    {
        //public List<ProductChildInitializer> GetDataListForChildProduct
        //{
        //    get
        //    {
        //        return new ProductChildtDataArray().DataArray();
        //    }
        //}

        //public override void AddInitData()
        //{
        //    //get the data
        //    List<ProductChildInitializer> dataList = GetDataListForChildProduct;

        //    if (dataList.IsNullOrEmpty())
        //    {
        //        ErrorsGlobal.Add("No data available.", MethodBase.GetCurrentMethod());
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }


        //    foreach (ProductChildInitializer item in dataList)
        //    {
        //        ProductChild pc = new ProductChild();

        //        pc.Name = item.ProductName;
        //        pc.Sell.SellPrice = item.SalePrice;
        //        Product product = ProductBiz
        //        pc.ProductId
        //        CreateAndSave(pc);
        //    }
        //}

        //private void getUser(ProductChildInitializer item, ProductChild pc)
        //{
        //    //get user
        //    ApplicationUser user = UserDal.FindAll().FirstOrDefault(x =>
        //        x.UserName.ToLower() == item.UserName.ToLower());

        //    if (user.IsNull())
        //    {
        //        ErrorsGlobal.Add(string.Format("User '{0}' Not found. Erronious starting data.", item.UserName), MethodBase.GetCurrentMethod());
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }

        //    pc.User = user;
        //    pc.UserId = user.Id;
        //}

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

    }
}
