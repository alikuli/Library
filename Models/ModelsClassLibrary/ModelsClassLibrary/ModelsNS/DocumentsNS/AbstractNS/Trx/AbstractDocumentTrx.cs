using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using InterfacesLibrary.ProductNS;
using InterfacesLibrary.DocumentsNS;
using InterfacesLibrary.SharedNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{

    public abstract class AbstractDocumentTrx : CommonWithId, IAbstractDocumentTrx
    {

        protected const string NOT_ALLOWED_STATEMENT_FOR_FORCED_SALE = "Not allowed. To continue with this transaction as it is, mark it as forced. Note: You will be reported for this forced transaction to the administrator.";



        #region Constructor

        public AbstractDocumentTrx()
        {
            IsForcedSale = false;

            TotalShippedQty = new CounterClass() as ICounterClass;
            FinalSalesPrice = new CounterClass() as ICounterClass;
            LineTotal_Money_Ordered = new CounterClass() as ICounterClass;
            LineTotal_Money_Ship = new CounterClass() as ICounterClass;
            QtyRemaining = new CounterClass() as ICounterClass;

            //TotalShippedQty.Calculator(Calculator_TotalShippedQty); //Diff in SalesOrderTrx/InvoiceTrx
            //FinalSalesPrice.Calculator(Calculator_FinalSalesPrice);
            //LineTotal_Money_Ordered.Calculator(Calculator_LineTotal_Money_Ordered);
            //LineTotal_Money_Ship.Calculator(Calculator_LineTotal_Money_Ship);
            //QtyRemaining.Calculator(Calculator_QtyRemaining);
        }

        #endregion



        #region Properties

        #region Product


        public Guid ProductID { get; set; }
        public IProduct Product { get; set; }


        [MaxLength(1000, ErrorMessage = "Max length allowed is {0} charecters")]
        public string Description { get; set; }



        #endregion

        #region Prices and discounts


        /// <summary>
        /// This is the listed price for the product. I save it for future reference
        /// </summary>
        [Display(Name = "Listed Price")]
        public decimal ListedPrice { get; set; }

        /// <summary>
        /// This is the calculated discounted price which has come from the discount program
        /// </summary>
        [Display(Name = "Price")]
        public decimal SalePrice { get; set; }


        [Display(Name = "BuyPrice")]
        public decimal CurrBuyPrice { get; set; }

        /// This is an additional discount over the sale price.
        /// </summary>
        [Display(Name = "Discount %")]
        public decimal DiscountPct { get; set; }


        #endregion

        #region Counter Classes




        /// <summary>
        /// This is the total quantity shipped for this sales order
        /// </summary>
        [NotMapped]
        public ICounterClass TotalShippedQty { get; set; }

        [NotMapped]
        [Display(Name = "Remaining")]
        public ICounterClass QtyRemaining { get; set; }


        /// <summary>
        /// This is the calculated sale price. This can also be used in Invoice because 
        /// invoice has a discount (even if it is hidden)
        /// </summary>
        [NotMapped]
        public ICounterClass FinalSalesPrice { get; set; }


        /// <summary>
        /// This is the total ordered in money
        /// </summary>
        [NotMapped]
        public ICounterClass LineTotal_Money_Ordered { get; set; }


        /// <summary>
        /// This is the total shipped for the line item in money
        /// </summary>
        [NotMapped]
        public ICounterClass LineTotal_Money_Ship { get; set; }


        //public CounterClass TotalQtyShipped { get; set; }


        #endregion


        #region Counter Class Delegates



        protected virtual decimal Calculator_LineTotal_Money_Ship()
        {
            return ShipQty * FinalSalesPrice.Amount;
        }

        protected virtual decimal Calculator_LineTotal_Money_Ordered()
        {
            return OrderedQty * FinalSalesPrice.Amount;
        }

        protected virtual decimal Calculator_FinalSalesPrice()
        {
            return SalePrice * (1 - DiscountPct);
        }

        protected virtual decimal Calculator_QtyRemaining()
        {
            //Note. The TotalShippedQty is calculated in the
            //child class, SalesOrderTrx/InvoiceTrx
            return OrderedQty - TotalShippedQty.Amount;
        }

        /// <summary>
        /// This returs ShipQty. It is giving the amount for the Invoice Trx.
        /// </summary>
        /// <returns></returns>
        protected virtual decimal Calculator_TotalShippedQty()
        {
            return ShipQty;
        }


        #endregion


        #region Quantities ...


        /// <summary>
        /// This is the quantity being ordered in Invoice and Sale order
        /// </summary>
        [Display(Name = "Order Qty")]
        public decimal OrderedQty { get; set; }


        /// <summary>
        /// This is the quantity that ships in the invoice and the sale order
        /// </summary>
        [Display(Name = "Ship Qty")]
        public decimal ShipQty { get; set; }


        #endregion

        /// <summary>
        /// <summary>
        /// If this is true, it overrides certain checks. However, we want the transaction reported if this is used.
        /// this is also moved to the invoicetrx for record purposes.
        /// </summary>
        [Display(Name = "Force")]
        public bool IsForcedSale { get; set; }

        #region dates

        /// <summary>
        /// This is the date that shipment begins. Can be null
        /// </summary>
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Date To Ship Start")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateToShipBegin { get; set; }



        /// <summary>
        /// This is the date that shipment ends. Can be null
        /// </summary>
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Date To Ship End")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateToShipEnd { get; set; }

        #endregion
        
        #endregion

        #region Price and Totals

        //public override string ToString()
        //{
        //    string itemNoAndDescription = string.Format("{0} {1}", Product.ItemNos, Product.ShortDescription);
        //    string s = string.Format("{0} Ordered:{1:0.00}/{5:C} Shipped:{2:0.00}/{6:C} Discount: {3:P2} Listed: {4:C} SalePrice: {7:C}",
        //        itemNoAndDescription,  //0
        //        QtyOrdered,            //1
        //        QtyShipped,            //2
        //        DiscountPct,           //3
        //        ListedPrice,             //4
        //        LineTotalOrderedCalculator, //5
        //        LineTotalShippedCalculator,          //6
        //        SalePrice);   //7
        //    return base.ToString();
        //}




        #endregion

        #region SelfErrorCheck



        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

            Check_Product_Not_Empty();
            Check_Description_Not_Empty();
            Check_If_Amount_Is_Negative_Force_Credit_Order_Ordered();
            Check_If_Amount_Is_Negative_Force_Credit_Order_Shipped();
            Check_DateToShipBegin_Not_Greater_Than_DateExoectedEnd();
            Check_If_Product_Active();
            Check_Curr_Buy_Price();
            if (!IsForcedSale)
            {
                Check_Discount_Not_greater_Than_100pct();
                Check_Qty_Ordered_Not_zero();
                Check_Product_Price_is_above_cost();
            }
        }



        



        #region SelfErrorCheck Helpers

        protected virtual void Check_If_Product_Active()
        {
            if (!IsProductActive)
            {
                throw new Exception(string.Format("Product '{0}' is not active. AbstractDocumentTrx.Check_If_Product_Active",
                    GetProductNameFromProduct()));
            }
        }
        protected virtual void Check_Curr_Buy_Price()
        {
            throw new NotImplementedException(MetaData.GetSelfClassName() + " Not implemented");
        }

        protected virtual void Check_DateToShipBegin_Not_Greater_Than_DateExoectedEnd()
        {
            if (!IsDateToShiped_BeforeOrEqualTo_DateExpectedEnd)
            {
                throw new Exception(string.Format("Error. Date To Ship '{0}' Beginis AFTER Date To Ship End '{1}. AbstractDocumentTrx.Check_DateToShipBegin_Not_Greater_Than_DateExoectedEnd'",
                    DateToShipBegin,
                    DateToShipEnd));
            }
        }


        protected virtual void Check_If_Amount_Is_Negative_Force_Credit_Order_Shipped()
        {
            if (ShipQty < 0)
            {
                throw new Exception(string.Format("Quantity shipped is '{0}' This is a negative number. If you want to issue a credit, please mark the sales order as 'CREDIT' and enter a positive number.  AbstractDocumentTrx.Check_If_Shipped_Amount_Is_Negative_Force_Credit_Order.",
                    ShipQty));

            }
        }

        protected virtual void Check_If_Amount_Is_Negative_Force_Credit_Order_Ordered()
        {
            if (OrderedQty < 0)
            {
                throw new Exception(string.Format("Quantity ordered is '{0}' This is a negative number. If you want to issue a credit, please mark the sales order as 'CREDIT' and enter a positive number.  AbstractDocumentTrx.Check_Qty_Shipped_Not_Greater_Than_Qty_Ordered.",
                    OrderedQty));

            }
        }

        protected virtual void Check_Discount_Not_greater_Than_100pct()
        {
            if (!IsForcedSale)
            {
                if (DiscountPct > 1)
                {
                    throw new Exception(string.Format("Your discount is '{0:P2}' which is greater than 100%. {1}. AbstractDocumentTrx.Check_Discount_Not_greater_Than_100pct.",
                        DiscountPct,
                        NOT_ALLOWED_STATEMENT_FOR_FORCED_SALE));
                }
            }
        }

        //protected abstract void Check_Qty_Shipped_Not_Greater_Than_Qty_Ordered();

        //protected virtual void Check_Qty_Shipped_Not_Greater_Than_Qty_Ordered()
        //{
        //    if (!IsForcedSale)
        //    {
        //        if (QtyOrdered < QtyToShip)
        //        {
        //            throw new Exception(string.Format("Quantity ordered is '{0}' Quantity To Ship: '{1}'. You are shipping more than the ordered amount!.  {2}. AbstractDocumentTrx.Check_Qty_Shipped_Not_Greater_Than_Qty_Ordered.",
        //                QtyOrdered,
        //                QtyToShip,
        //                NOT_ALLOWED_STATEMENT_FOR_FORCED_SALE));
        //        }
        //    }
        //}

        protected virtual void Check_Qty_Ordered_Not_zero()
        {
            if (!IsForcedSale)
            {
                if (OrderedQty == 0)
                {
                    throw new Exception(string.Format("Quantity ordered is '{0}' You have placed a zero order.  {1}. AbstractDocumentTrx.Check_Qty_Shipped_Not_Greater_Than_Qty_Ordered.",
                        OrderedQty,
                        NOT_ALLOWED_STATEMENT_FOR_FORCED_SALE));
                }
            }
        }

        protected virtual void Check_Description_Not_Empty()
        {
            if (Description.IsNullOrEmpty())
            {
                Description = Product.Name;

                throw new Exception(string.Format("The description is empty. This is not allowed. We have automaticly added the product default description: '{0}'. Please try again or re-fix the description. AbstractDocumentTrx.Check_Description_Not_Empty.",
                    Description));

            }
        }

        protected virtual void Check_Product_Price_is_above_cost()
        {
            if (!IsForcedSale)
            {
                if (GetFinalSalePrice () < Product.BuyPrice)
                {
                    throw new Exception(string.Format("The sale price '{0}' of Product: '{1} is less than its buy price: '{2}.' {3}. .Check_Product_Price_is_above_cost",
                        GetFinalSalePrice (),
                        Product.FullName(),
                        Product.BuyPrice,
                        NOT_ALLOWED_STATEMENT_FOR_FORCED_SALE));
                }
            }
        }


        protected virtual void Check_Product_Not_Empty()
        {
            //Product Id cannot be empty
            if (ProductID.IsNullOrEmpty())
            {
                throw new Exception("No product selected. 01.AbstractDocumentTrx.SalesOrderTrx");
            }

            if (Product == null)
            {
                throw new Exception("No product selected. 02.AbstractDocumentTrx.SalesOrderTrx");

            }
        }
        #endregion

        #endregion

        #region Is...

        protected bool IsProductActive
        {
            get
            {
                Check_Product_Not_Empty();
                return Product.MetaData.IsActive;
            }
        }


        public bool IsDateToShiped_BeforeOrEqualTo_DateExpectedEnd
        {
            get
            {
                DateTime dateToShipBegin = DateToShipBegin ?? DateTime.MinValue;
                DateTime dateToShipEnd = DateToShipEnd ?? DateTime.MaxValue;

                return dateToShipBegin.Date <= dateToShipEnd.Date;
            }
        }



        

        #endregion

        #region Get...
        public string GetProductNameFromProduct()
        {
            Check_Product_Not_Empty();
            return Product.Name;
        }
        public decimal GetProduct_ListedSellPrice()
        {
            Check_Product_Not_Empty();
            return ((Product)Product).SellPrice;
        }

        //public decimal GetProduct_ListedSellPrice()
        //{
        //    Check_Product_Not_Empty();
        //    return Product.SellPrice;
        //}
        public decimal GetProduct_BuyPrice()
        {
            Check_Product_Not_Empty();
            return Product.BuyPrice;
        }

        public decimal GetProduct_MlpPrice()
        {
            Check_Product_Not_Empty();
            return ((Product)Product).MlpPrice;
        }

        public decimal GetFinalSalePrice()
        {
            throw new NotImplementedException();
        }

        public decimal GetLineTotal_Ordered()
        {
            throw new NotImplementedException();
        }
        public decimal GetLineTotal_Shipped()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Loaders
        /// <summary>
        /// This loads all the data of a into this.
        /// </summary>
        /// <param name="a"></param>
        public void LoadFrom(AbstractDocumentTrx a)
        {
            base.LoadFrom(a as CommonWithId);

            CurrBuyPrice = a.CurrBuyPrice;
            DateToShipBegin = a.DateToShipBegin;
            DateToShipEnd = a.DateToShipEnd;
            Description = a.Description;
            DiscountPct = a.DiscountPct;
            ListedPrice = a.ListedPrice;
            OrderedQty = a.OrderedQty;
            Product = a.Product;
            ProductID = a.ProductID;
            SalePrice = a.SalePrice;
            ShipQty = a.ShipQty;
        }
        
        #endregion
      
    }
}
