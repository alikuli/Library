using AliKuli.Extentions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    [ComplexType]
    public class SalePriceComplex
    {
        /// <summary>
        /// Manufacturers suggested price
        /// </summary>
        [Display(Name = "Suggested Price")]
        public decimal MSRP { get; set; }

        [Display(Name = "Lowest Price")]
        public decimal MlpPrice { get; set; }


        [Display(Name = "Sell Price")]
        public decimal SellPrice { get; set; }

        [NotMapped]
        public string SellPriceMoneyFormat { get { return ToMoneyFormat(SellPrice); } }

        public string ToMoneyFormat(decimal money)
        {
            return money.ToString().ToRuppeeFormat();
        }

        [NotMapped]
        [Display(Name = "Highest Sell Price")]
        public decimal HighestSalePrice { get; set; }

        [NotMapped]
        [Display(Name = "Highest Sell Price Date")]
        public DateTime HighestSalePriceDate { get; set; }

        [NotMapped]
        [Display(Name = "Lowest Sell Price")]
        public decimal LowestSalePrice { get; set; }

        [NotMapped]
        [Display(Name = "Lowest Sell Price Date")]
        public DateTime LowestSalePriceDate { get; set; }

        [NotMapped]
        [Display(Name = "Avg Sell Price")]
        public decimal AverageSalePrice { get; set; }

        [NotMapped]
        public bool IsCalculated { get; set; }
        public void LoadCalculateFields(decimal highestSalePrice, DateTime highestSalePriceDate, decimal lowestSalePrice, DateTime lowestSalePriceDate, decimal avgSalePrice)
        {
            HighestSalePrice = highestSalePrice;
            HighestSalePriceDate = highestSalePriceDate;
            LowestSalePrice = lowestSalePrice;
            LowestSalePriceDate = lowestSalePriceDate;
            AverageSalePrice = avgSalePrice;
            IsCalculated = true;
        }

    }
}
