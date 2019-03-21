using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.AbstractNS
{
    public abstract partial class DocumentAbstract : CommonWithId
    {

        public DocumentAbstract()
        {

        }

        public DocumentAbstract(
            string ownerId,
            string customerId,
            string addressInformToId,
            string addressShipToId,
            string poNumber,
            DateTime poDate,
            SelectList selectListOwner,
            SelectList selectListCustomer,
            SelectList selectListAddressInformTo,
            SelectList selectListAddressShipTo)
        {
            InitializeAbstract(
                ownerId,
                customerId,
                addressInformToId,
                addressShipToId,
                poNumber,
                poDate,
                selectListOwner,
                selectListCustomer,
                selectListAddressInformTo,
                selectListAddressShipTo);
        }


        protected virtual void InitializeAbstract(
            string ownerId,
            string customerId,
            string addressInformToId,
            string addressShipToId,
            string poNumber,
            DateTime poDate,
            SelectList selectListOwner,
            SelectList selectListCustomer,
            SelectList selectListAddressInformTo,
            SelectList selectListAddressShipTo)
        {
            OwnerId = ownerId;
            CustomerId = customerId;
            AddressInformToId = addressInformToId;
            AddressShipToId = addressShipToId;
            PoNumber = poNumber;
            PoDate = poDate;
            SelectListOwner = selectListOwner;
            SelectListCustomer = selectListCustomer;
            SelectListAddressInformTo = selectListAddressInformTo;
            SelectListAddressShipTo = selectListAddressShipTo;
        }

        [Display(Name = "Document Number")]
        public long DocumentNumber { get; set; }

        [Display(Name = "Owner")]
        public string OwnerId { get; set; }

        [Display(Name = "Owner")]
        public virtual Owner Owner { get; set; }


        [Display(Name = "Customer")]
        public string CustomerId { get; set; }


        [Display(Name = "Customer")]
        public virtual Customer Customer { get; set; }

        [Display(Name = "Inform To")]
        public string AddressInformToId { get; set; }

        [Display(Name = "Inform To")]
        public virtual AddressMain AddressInformTo { get; set; }

        [Display(Name = "Address To")]
        public string AddressShipToId { get; set; }

        [Display(Name = "Address To")]
        public virtual AddressMain AddressShipTo { get; set; }


        [NotMapped]
        public SelectList SelectListOwner { get; set; }

        [NotMapped]
        public SelectList SelectListCustomer { get; set; }

        [NotMapped]
        public SelectList SelectListAddressInformTo { get; set; }

        [NotMapped]
        public SelectList SelectListAddressShipTo { get; set; }


        [Display(Name = "PO #")]
        public string PoNumber { get; set; }


        [Display(Name = "PO Date")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PoDate { get; set; }

        [Display(Name = "Last Ship Date")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastShipDate { get; set; }

        public override string MakeUniqueName()
        {
            if (DocumentNumber == 0)
                throw new Exception("Document Number is Zero!");

            string uniqueName = string.Format("{0}", DocumentNumber);
            return uniqueName;
        }


    }
}
