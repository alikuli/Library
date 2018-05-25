﻿using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
namespace ModelsClassLibrary.ModelsNS.ProductNS
{

    public abstract partial class ProductAbstract : CommonWithId
    {


        //[Display(Name = "Uom For Packing Required?")]
        //public bool IsUomForPackingVolRequired { get; set; }


        //[Display(Name = "Zero MSRP Allowed?")]
        //public bool IsAllowd_Zero_MRSP { get; set; }


        /// <summary>
        /// If this is a child, then we will need the parent.
        /// </summary>
        [Display(Name = "Child?")]
        public bool IsChild
        {
            get
            {
                return !ParentId.IsNullOrWhiteSpace();
            }
        }


        /// <summary>
        /// If true, product will be displayed on Website.
        /// </summary>
        [Display(Name = "Display on Website?")]
        public bool IsDisplayedOnWebsite { get; set; }




    }
}