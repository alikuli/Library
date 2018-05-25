using Bearer.DAL;
using Bearer.Models;
using Bearer6.DAL;
using Bearer6.Models.PlayersNS;
using ModelsClassLibrary.Models.AddressNS;
using ModelsClassLibrary.Models.DeliveryMethodNS;
using ModelsClassLibrary.Models.Documents.Sale;
using ModelsClassLibrary.Models.Documents.Sale.SalesorderNS;
using ModelsClassLibrary.Models.PlayersNS;
using System;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeSalesOrders
    {
        private static SalesOrderDAL _dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializeSalesOrders(ApplicationDbContext db, string user)
        {
            _db = db;
            _user = user;
            _dal = new SalesOrderDAL(db, _user);
        }
        #region Add
        private void Add(
            string comment,
            Customer consignTo,
            Guid? consignToID,
            DeliveryMethod deliveryMethod,
            Guid? deliveryMethodID,
            DateTime expectedDate,
            AddressWithTownClass informTo,
            Guid? informToID,
            decimal miscPaymentAmount,
            Owner owner,
            Guid? ownerID,
            PaymentMethod paymentMethod,
            Guid? paymentMethodID,
            PaymentTerm paymentTerm,
            Guid? paymentTermID,
            SaleTypeEnum saleOrderTypeENUM,
            Salesman salesman,
            Guid? salesmanID,
            decimal shippingAmount,
            AddressWithTownClass shipTo,
            Guid? shipToID,
            decimal taxAmount,
            string theirPurchaseOrderNumber)
        {

            try
            {
                _dal.CreateAutomaticly(
                    comment,
                    consignTo,
                    consignToID,
                    deliveryMethod,
                    deliveryMethodID,
                    expectedDate,
                    informTo,
                    informToID,
                    miscPaymentAmount,
                    owner,
                    ownerID,
                    paymentMethod,
                    paymentMethodID,
                    paymentTerm,
                    paymentTermID,
                    saleOrderTypeENUM,
                    salesman,
                    salesmanID,
                    shippingAmount,
                    shipTo,
                    shipToID,
                    taxAmount,
                    theirPurchaseOrderNumber);

                _dal.Save();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {

            }
        }

        #endregion
        public void Initialize()
        {

            //Make some with classes going in... others with Ids


            #region Setup DALs
            CustomerDAL custDAL = new CustomerDAL(_db, _user);
            OwnerDAL ownerDAL = new OwnerDAL(_db, _user);
            SalesmanDAL salesmanDAL = new SalesmanDAL(_db, _user);
            AddressDAL addyDAL = new AddressDAL(_db, _user);

            PaymentMethodDAL payMethodDAL = new PaymentMethodDAL(_db, _user);
            PaymentTermDAL payTermDAL = new PaymentTermDAL(_db, _user);
            DeliveryMethodDAL delyMethodDAL = new DeliveryMethodDAL(_db, _user);

            #endregion

            #region Methodwide Declarations

            Customer[] custArray;
            Owner[] ownerArray;
            Salesman[] salesmanArray;
            PaymentTerm[] payTermArray;
            PaymentMethod[] payMethodArray;
            DeliveryMethod[] delyMethodArray;

            Customer currCustomer;
            Owner currOwner;
            Salesman currSalesman;
            DeliveryMethod currDeliveryMethod;
            PaymentMethod currPaymentMethod;
            PaymentTerm currPaymentTerm;
            AddressWithTownClass currShipToAddress;
            AddressWithTownClass currInformToAddress;
            string executing = "";
            string currComment = "";
            string currTheirPoNumber = "";
            int arrayValue = 0;

            #endregion


            #region Setup Arrays

            try
            {
                executing = "CustArray";
                custArray = custDAL.FindAll().ToArray();
            
                executing = "OwnerArray"; 
                ownerArray = ownerDAL.FindAll().ToArray();

                executing = "SalesmanArray";
                salesmanArray = salesmanDAL.FindAll().ToArray();
                
                executing = "payTermArray";
                payTermArray = payTermDAL.FindAll().ToArray();
                
                executing = "payMethodArray";
                payMethodArray = payMethodDAL.FindAll().ToArray();
                
                executing = "delyMethodArray";
                delyMethodArray = delyMethodDAL.FindAll().ToArray();

            }
            catch(Exception e)
            {
                string error = AliKuli.Utilities.ExceptionNS.ErrorMsgClass.GetInnerException(e) + " - " + executing;
                throw new Exception(error);
            }
            #endregion

            #region Setup 1 Complete Set

            

            currComment = "Initialize 1";
            currTheirPoNumber = "PO-1234";
            currCustomer = custArray[arrayValue];
            currOwner = ownerArray[arrayValue+1];
            currSalesman = salesmanArray[arrayValue];
            currDeliveryMethod = delyMethodArray[arrayValue];
            currPaymentMethod = payMethodArray[arrayValue];
            currPaymentTerm = payTermArray[arrayValue];
            currPaymentMethod = payMethodArray[arrayValue];
            currShipToAddress = currCustomer.AddressFromUser;
            currInformToAddress = currCustomer.AddressFromUser;
            Guid? currCustGuid = null;
            Guid? currDelyMethodGuid = null;
            Guid? currInformToGuid = null;
            Guid? currOwnerGuid = null;
            Guid? currPaymentMethodGuid = null;
            Guid? currPaymentTermGuid = null;
            Guid? currSalesmanGuid = null;
            Guid? currShipToAddressGuid = null;
            SaleTypeEnum currSaleOrderTypeEnum = SaleTypeEnum.Sale;
            DateTime currEstDeliverDate = DateTime.MinValue;
            decimal currMiscCharges = 20;
            decimal currshippingAmount = 20;
            decimal currTaxAmount  = 10;
            
            Add(
                currComment, 
                currCustomer, 
                currCustGuid, 
                currDeliveryMethod, 
                currDelyMethodGuid, 
                currEstDeliverDate,
                currInformToAddress,
                currInformToGuid,
                currMiscCharges, 
                currOwner, 
                currOwnerGuid,
                currPaymentMethod, 
                currPaymentMethodGuid, 
                currPaymentTerm, 
                currPaymentTermGuid, 
                currSaleOrderTypeEnum, 
                currSalesman, 
                currSalesmanGuid, 
                currshippingAmount, 
                currShipToAddress, 
                currShipToAddressGuid, 
                currTaxAmount, 
                currTheirPoNumber);

            #endregion


            #region Setup 2 Complete Set



            currComment = "Initialize 2";
            currTheirPoNumber = "PO-4321";
            arrayValue = 1;
            currCustomer = custArray[arrayValue];
            currOwner = ownerArray[arrayValue - 1];
            currSalesman = salesmanArray[arrayValue];
            currDeliveryMethod = delyMethodArray[arrayValue];
            currPaymentMethod = payMethodArray[arrayValue];
            currPaymentTerm = payTermArray[arrayValue];
            currPaymentMethod = payMethodArray[arrayValue];
            currShipToAddress = currCustomer.AddressFromUser;
            currInformToAddress = currCustomer.AddressFromUser;
            currCustGuid = null;
            currDelyMethodGuid = null;
            currInformToGuid = null;
            currOwnerGuid = null;
            currPaymentMethodGuid = null;
            currPaymentTermGuid = null;
            currSalesmanGuid = null;
            currShipToAddressGuid = null;
            currSaleOrderTypeEnum = SaleTypeEnum.Quotation;
            currEstDeliverDate = DateTime.Now.AddDays(19);
            currMiscCharges = 230;
            currshippingAmount = 120;
            currTaxAmount = 100;

            Add(
                currComment,
                currCustomer,
                currCustGuid,
                currDeliveryMethod,
                currDelyMethodGuid,
                currEstDeliverDate,
                currInformToAddress,
                currInformToGuid,
                currMiscCharges,
                currOwner,
                currOwnerGuid,
                currPaymentMethod,
                currPaymentMethodGuid,
                currPaymentTerm,
                currPaymentTermGuid,
                currSaleOrderTypeEnum,
                currSalesman,
                currSalesmanGuid,
                currshippingAmount,
                currShipToAddress,
                currShipToAddressGuid,
                currTaxAmount,
                currTheirPoNumber);

            #endregion



            #region Setup 3 Complete Set



            currComment = "Initialize 3";
            currTheirPoNumber = "PO-5324";
            arrayValue = 0;

            currCustomer = custArray[arrayValue];
            currOwner = ownerArray[arrayValue + 1];
            currSalesman = salesmanArray[arrayValue];
            currDeliveryMethod = delyMethodArray[arrayValue];
            currPaymentMethod = payMethodArray[arrayValue];
            currPaymentTerm = payTermArray[arrayValue];
            //currShipToAddress = currCustomer.AddressFromUser;
            //currInformToAddress = currCustomer.AddressFromUser;

            currCustGuid = custArray[arrayValue].Id;
            currOwnerGuid = ownerArray[arrayValue + 1].Id;
            currSalesmanGuid = salesmanArray[arrayValue].Id;
            currDelyMethodGuid = delyMethodArray[arrayValue].Id;
            currPaymentMethodGuid = payMethodArray[arrayValue].Id;
            currPaymentTermGuid = payTermArray[arrayValue].Id;
            currShipToAddressGuid = null;
            currInformToGuid = null;


            currShipToAddress = currCustomer.AddressFromUser;
            currInformToAddress = currCustomer.AddressFromUser;

            currSaleOrderTypeEnum = SaleTypeEnum.Backorder;
            currEstDeliverDate = DateTime.Now.AddDays(19);
            currMiscCharges = 34;
            currshippingAmount = 1232;
            currTaxAmount = 10;

            Add(
                currComment,
                currCustomer,
                currCustGuid,
                currDeliveryMethod,
                currDelyMethodGuid,
                currEstDeliverDate,
                currInformToAddress,
                currInformToGuid,
                currMiscCharges,
                currOwner,
                currOwnerGuid,
                currPaymentMethod,
                currPaymentMethodGuid,
                currPaymentTerm,
                currPaymentTermGuid,
                currSaleOrderTypeEnum,
                currSalesman,
                currSalesmanGuid,
                currshippingAmount,
                currShipToAddress,
                currShipToAddressGuid,
                currTaxAmount,
                currTheirPoNumber);

            #endregion

        }


        public void DeleteAll()
        {
            var iqlistSalesOrders = _dal.FindAll().ToList();

            foreach (SalesOrder item in iqlistSalesOrders)
            {
                //first delete the transactions
                //var trxs = item.SalesOrderTrxs.ToList();
                //foreach (SalesorderTrx soTrx in trxs)
                //{
                //    db.SalesorderTrxes.Remove(soTrx);
                //}
                _db.SalesOrders.Remove(item);
            }
            _db.SaveChanges();

            iqlistSalesOrders = _dal.FindAll(true).ToList();
            foreach (var item in iqlistSalesOrders)
            {
                //first delete the transactions
                //var trxs = item.SalesOrderTrxs.ToList();
                //foreach (SalesorderTrx soTrx in trxs)
                //{
                //    db.SalesorderTrxes.Remove(soTrx);
                //}
                _db.SalesOrders.Remove(item);

            }
            _db.SaveChanges();
        }

    }
}