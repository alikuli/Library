using AliKuli.Extentions;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using Ninject;
using NUnit.Framework;
using UowLibrary.BuySellDocNS;

namespace UnitTestProject1
{
    [TestClass]
    public class BuySellItemBiz_Tests
    {

        //public ApplicationDb
        //https://oncodedesign.com/unit-testing-on-top-of-entity-framework-dbcontext/

        //public IRepositry<BuySellItem> Repo_BuySellItem()
        //{
        //    return new Repositry<BuySellItem>()
        //}
        //public BuySellDocBiz BuySellItemBiz_Factory()
        //{
        //    return new BuySellItemBiz();
        //}

        //[Test]
        //public void getSalePriceFor_Non_Zero_Value_Return_True()
        //{
        //    //IBusinessLayer<BuySellItem> buySellItemBiz = new BuySellItemBiz() as IBusinessLayer<BuySellItem>;

        //    //BuySellItemBiz buySellItemBiz = BuySellItemBiz;
        //    //buySellItemBiz.IsNullThrowException();
        //    //BuySellItem bsi = buySellItemBiz.Factory() as BuySellItem;

        //    //bsi.SalePriceStr = "100";
        //    //bsi.SalePrice = 100M;
        //    //NUnit.Framework.Assert.AreEqual(100M, bsi.SalePrice);
        //}
        //[Test]
        //public void getSalePriceFor_Non_Zero_Value_Return_True()
        //{
        //    BuySellItem bsi = Factory() as BuySellItem;

        //    bsi.SalePriceStr = "100";
        //    bsi.SalePrice = 100M;
        //    NUnit.Framework.Assert.AreEqual(100M, bsi.SalePrice);
        //}

    }


    //public class BuySellItem_Repo: Repositry<BuySellItem>
    //{
    //    public BuySellItem_Repo():base()
    //    {

    //    }
    //}
}
