using EnumLibrary.EnumNS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public class UomQty : UomAbstract
    {
        public virtual ICollection<Product> Products_Purchase { get; set; }
        public virtual ICollection<Product> Products_Sale { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.UomQuantity;
        }

        //public ICollection<Product> ProductsWeightListedOnProductUoms { get; set; }
        //public ICollection<Product> ProductsPurchaseUoms { get; set; }
        //public ICollection<Product> ProductUomStocks { get; set; }
        //public ICollection<Product> ProductLastUomStocks { get; set; }
        //public ICollection<Product> ProductPrevToLastUomStocks { get; set; }
        //public ICollection<Product> ProductB4PrevToLastUomStocks { get; set; }



    }
}