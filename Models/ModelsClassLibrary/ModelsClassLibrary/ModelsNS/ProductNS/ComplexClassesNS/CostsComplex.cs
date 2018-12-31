using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS
{

    /// <summary>
    /// To ensure calculated fields have been calculated, check bool IsCalculated.
    /// </summary>
    [ComplexType]
    public class CostsComplex
    {

        [Display(Name = "Buy Cost")]
        public decimal Cost { get; set; }


        [NotMapped]
        public bool IsCalculated { get; set; }


        [Display(Name = "Avg Cost")]
        [NotMapped]
        public decimal AverageCost { get; set; }

        [Display(Name = "Highest Cost")]
        [NotMapped]
        public decimal HighestCost { get; set; }

        [Display(Name = "Date of Highest Cost")]
        [NotMapped]
        public DateTime HighestCostDate { get; set; }

        [Display(Name = "Lowest Cost")]
        [NotMapped]
        public decimal LowestCost { get; set; }

        [Display(Name = "Lowest Cost Date")]
        [NotMapped]
        public DateTime LowestCostDate { get; set; }

        public void LoadCalculateFields(decimal averageCost, decimal highestCost, DateTime highestCostDate, decimal lowestCost, DateTime lowestCostDate)
        {
            AverageCost = averageCost;
            HighestCost = highestCost;
            LowestCost = lowestCost;
            HighestCostDate = highestCostDate;
            LowestCostDate = lowestCostDate;

            IsCalculated = true;
        }
    }
}
