using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.ProductNS

{
    public abstract class ProductTrxAbstract:CommonWithId
    {
        #region Product
        /// <summary>
        /// this is the product for which this adjustment has been added
        /// </summary>
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
        #endregion
        #region Warehouse
        /// <summary>
        /// this is the warehouse for the transaction
        /// </summary>
        public Guid? WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        
        #endregion


        #region Date
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        
        #endregion

        #region Prices
        /// <summary>
        /// This is the current Msrp
        /// </summary>
        public decimal CurrMSRP { get; set; }

        /// <summary>
        /// This is the current Mlp Price
        /// </summary>
        public decimal CurrMlpPrice { get; set; }

        /// <summary>
        /// This is the current buy price
        /// </summary>
        public decimal CurrBuyPrice { get; set; }

        /// <summary>
        /// This is the current buy price average
        /// </summary>
        public decimal CurrBuyPriceAvg { get; set; }
        
        #endregion

        #region Quantity
		        /// <summary>
        /// This is the adjusting quantity. Negative means missing.
        /// </summary>
        public decimal AdjustingQuantity { get; set; }
 
	#endregion   

        public void LoadFrom(ProductTrxAbstract p)
        {
            Product = p.Product;
            ProductId = p.ProductId;
            Warehouse = p.Warehouse;
            WarehouseId = p.WarehouseId;
            Date = p.Date;
            CurrBuyPrice = p.CurrBuyPrice;
            CurrBuyPriceAvg = p.CurrBuyPriceAvg;
            CurrMlpPrice = p.CurrMlpPrice;
            CurrMSRP = p.CurrMSRP;
            AdjustingQuantity = p.AdjustingQuantity;

            ICommonWithId c = this as ICommonWithId;
            c.LoadFrom((ICommonWithId)p);
        }
    }
}