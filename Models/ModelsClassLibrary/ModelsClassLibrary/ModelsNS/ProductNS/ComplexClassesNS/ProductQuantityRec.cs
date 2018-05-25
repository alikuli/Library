using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    [ComplexType]
    public class Quantity
    {

        /// <summary>
        /// This will be calculated on the fly and loaded. This is the quantity that is in the warehouse. Not presisted.
        /// </summary>
        /// 
        [NotMapped]
        [Display(Name = "On Hand")]
        public double OnHand { get; set; }

        /// <summary>
        /// This is the amount that has been allotted/Booked. This amount cannot be booked again
        /// </summary>
        [NotMapped]
        [Display(Name = "Allotted")]

        public double Allotted { get; set; }

        /// <summary>
        /// This is the last allotted date -Not presisted, calculated.
        /// </summary>
        [Display(Name = "Last Allotted Date (UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [NotMapped]
        public DateTime LastAllottedDate { get; set; }


        /// <summary>
        /// This is the amount that is available. OnHand - Allotted
        /// </summary>
        [NotMapped]
        [Display(Name = "Available (on hand - sold)")]
        public double Available
        {
            get
            {
                return this.OnHand - this.Allotted;
            }
        }

        /// <summary>
        /// This is the amount that is on order. This is calculated on the fly.
        /// </summary>
        [NotMapped]
        [Display(Name = "On Order")]
        public double OnOrder { get; set; }


        /// <summary>
        /// This is the last Ordered date -Not presisted, calculated.
        /// </summary>
        [Display(Name = "Last Ordered Date (UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [NotMapped]
        public DateTime LastOrderedDate { get; set; }


        /// <summary>
        /// This is the amount projected. This is calculated on the fly.
        /// </summary>
        [NotMapped]
        [Display(Name = "Projected")]
        public double Projected { get; set; }



        /// <summary>
        /// This is the last count error. This is persisted.
        /// </summary>
        [Display(Name = "Prev Qty Error")]
        public double LastQtyError { get; set; }

        /// <summary>
        /// This is the UOM for the last count error... just in case the UOM changes in between the counts. This is presisted
        /// </summary>
        //[Display(Name = "Prev Error Uom")]
        //public UomQty LastUomStock { get; set; }
        public string LastUomStockName { get; set; }

        /// <summary>
        /// This is the date for the last count. This is persisted.
        /// </summary>
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Prev Error Date (UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime LastQtyErrorDate { get; set; }




        /// <summary>
        /// This is the error for the count previous to the last count. This is persisted.
        /// </summary>
        [Display(Name = "Prev To Last Qty Error")]
        public double PreviousToLastQtyError { get; set; }


        /// <summary>
        /// This is the Uom for the previous to last count, just in case the uom was changed. This is persisted.
        /// </summary>
        //[Display(Name = "Prev To Last Error Uom")]
        //public UomQty PreviousToLastUomStock { get; set; }
        public string  PreviousToLastUomStockName { get; set; }


        /// <summary>
        /// This is the date for the previous to Last count. This is persisted.
        /// </summary>
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Prev To Last Error Date (UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime PreviousToLastQtyErrorDate { get; set; }





        /// <summary>
        /// This is the error for the count previous to the last count. This is persisted.
        /// </summary>
        [Display(Name = "B4 Prev To Last Qty Error")]
        public double B4PreviousToLastQtyError { get; set; }


        /// <summary>
        /// This is the Uom for the previous to last count, just in case the uom was changed. This is persisted.
        /// </summary>
        //[Display(Name = "B4 Prev To Last Error Uom")]
        //public UomQty B4PreviousToLastUomStock { get; set; }
        public string B4PreviousToLastUomStockName { get; set; }


        /// <summary>
        /// This is the date for the previous to Last count. This is persisted.
        /// </summary>
        [Column(TypeName = "DateTime2")]
        [Display(Name = "B4 Prev To Last Error Date (UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime B4PreviousToLastQtyErrorDate { get; set; }

        public void UpdateErrorAmount(double errorAmount)
        {
            MoveDataFromLastCountToPreviousCount();
            LastQtyError = errorAmount;

        }
        private void MoveDataFromLastCountToPreviousCount()
        {
            this.B4PreviousToLastQtyError = this.PreviousToLastQtyError;
            this.B4PreviousToLastQtyErrorDate = this.PreviousToLastQtyErrorDate;
            this.B4PreviousToLastUomStockName = this.PreviousToLastUomStockName;

            this.PreviousToLastQtyError = this.LastQtyError;
            this.PreviousToLastQtyErrorDate = this.LastQtyErrorDate;
            this.PreviousToLastUomStockName = this.LastUomStockName;
        }

    }
}