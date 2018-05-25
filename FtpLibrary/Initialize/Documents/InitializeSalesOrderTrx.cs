using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.DeliveryMethodNS;
using ModelsClassLibrary.Models.Documents.Sale;
using ModelsClassLibrary.Models.Documents.Sale.SalesorderNS;
using ModelsClassLibrary.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeSalesOrderTrx
    {
        private static SalesOrderTrxDAL _dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializeSalesOrderTrx(ApplicationDbContext db, string userIn)
        {
            _db = db;
            _user=userIn;
            _dal = new SalesOrderTrxDAL(db, _user);
        }

        private void Add(            
            Guid? salesOrderId,
            SalesOrder salesOrder,
            Guid? productId,
            Product product,
            decimal qtyOrdered,
            decimal qtyToShip,
            decimal salePrice,
            DateTime dateToShipBegin,
            DateTime dateToShipEnd,
            string comment)
        {

            SalesOrderTrx entity = _dal.AutoCreate(salesOrderId, salesOrder, productId, product, qtyOrdered, qtyToShip, salePrice, dateToShipBegin,dateToShipEnd, comment);

            try
            {
                _dal.Create(entity);
                _dal.Save();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            { }

            catch
            {
                throw;
            }
        }
        

        public void Initialize()
        {
            ProductDAL prodDAL = new ProductDAL(_db, _user);
            SalesOrderDAL soDAL = new SalesOrderDAL(_db, _user);

            IQueryable<Product> lstProduct = prodDAL.FindAll();
            IQueryable<SalesOrder> lstSalesOrder = soDAL.FindAll();


            Product[] arrayProduct = lstProduct.ToArray();
            SalesOrder[] arraySo = lstSalesOrder.ToArray();

            Guid? salesOrderId = null;
            Guid? productId = null;
            SalesOrder salesOrder = arraySo[0];
            Product product =arrayProduct [0];
            decimal qtyOrdered = 10;
            decimal qtyToShip = 9;
            decimal sellPrice = product.SellPrice;
            string comment = "First Initialization";
            DateTime dateToShipBegin = DateTime.UtcNow;
            DateTime dateToShipEnd = DateTime.UtcNow.AddDays(7);
            Add(salesOrderId, salesOrder, productId, product, qtyOrdered, qtyToShip, sellPrice, dateToShipBegin, dateToShipEnd, comment);



            salesOrderId = null;
            productId = null;
            salesOrder = arraySo[1];
            product = arrayProduct[1];
            qtyOrdered = 10;
            qtyToShip = 9;
            sellPrice = product.SellPrice;
            comment = "2nd Initialization";
            dateToShipBegin = DateTime.UtcNow.AddDays(10);
            dateToShipEnd = dateToShipBegin.AddDays(7);
            Add(salesOrderId, salesOrder, productId, product, qtyOrdered, qtyToShip, sellPrice, dateToShipBegin, dateToShipEnd, comment);




            salesOrderId = null;
            productId = null;
            salesOrder = arraySo[2];
            product = arrayProduct[2];
            qtyOrdered = 10;
            qtyToShip = 9;
            sellPrice = product.SellPrice;
            comment = "2nd Initialization";
            dateToShipBegin = DateTime.UtcNow.AddDays(100);
            dateToShipEnd = dateToShipBegin.AddDays(12);
            Add(salesOrderId, salesOrder, productId, product, qtyOrdered, qtyToShip, sellPrice, dateToShipBegin, dateToShipEnd, comment);



            salesOrderId = arraySo[2].Id;
            productId = arrayProduct[2].Id;
            salesOrder = null;
            product = null;
            qtyOrdered = 34;
            qtyToShip = 15;
            sellPrice = 399;
            comment = "3rd Initialization";
            dateToShipBegin = DateTime.UtcNow.AddDays(155);
            dateToShipEnd = dateToShipBegin.AddDays(17);
            Add(salesOrderId, salesOrder, productId, product, qtyOrdered, qtyToShip, sellPrice, dateToShipBegin, dateToShipEnd, comment);
        }
        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                _db.SalesorderTrxes.Remove(item);
            }
            _db.SaveChanges();

            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                _db.SalesorderTrxes.Remove(item);
            }
            _db.SaveChanges();
        }

        public void Edit()
        {

            //List<PaymentTerm> lst = _dal.FindAll().ToList();

            //if (lst == null)
            //    throw new Exception(string.Format("No '{0}' found to edit.", _dal.GetSelfClassName()));

            //foreach (var item in lst)
            //{
            //    item.Name += "*";
            //    _dal.Update(item);
            //    try
            //    {
            //        _dal.Save();
            //    }
            //    catch (Exception e)
            //    {
            //        string error = AliKuli.Utilities.ExceptionNS.ErrorMsgClass.GetInnerException(e) + " - For item: " + item.Name;
            //    }
            //}

        }

    }
}