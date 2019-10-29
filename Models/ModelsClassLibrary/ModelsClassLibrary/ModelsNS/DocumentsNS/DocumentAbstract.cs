using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
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
            string addressBillToId,
            string addressShipToId,
            SelectList selectListOwner,
            SelectList selectListCustomer,
            SelectList selectListAddressInformTo,
            SelectList selectListAddressShipTo)
        {
            InitializeAbstract(
                ownerId,
                customerId,
                addressBillToId,
                addressShipToId,
                selectListOwner,
                selectListCustomer,
                selectListAddressInformTo,
                selectListAddressShipTo);
        }


        protected virtual void InitializeAbstract(
            string ownerId,
            string customerId,
            string addressBillToId,
            string addressShipToId,
            SelectList selectListOwner,
            SelectList selectListCustomer,
            SelectList selectListAddressInformTo,
            SelectList selectListAddressShipTo)
        {
            OwnerId = ownerId;
            CustomerId = customerId;
            AddressBillToId = addressBillToId;
            AddressShipToId = addressShipToId;
            SelectListOwner = selectListOwner;
            SelectListCustomer = selectListCustomer;
            SelectListAddressInformTo = selectListAddressInformTo;
            SelectListAddressShipTo = selectListAddressShipTo;
        }

        [Display(Name = "Document Number")]
        public long DocumentNumber { get; set; }




        [Display(Name = "Vendor (Seller)")]
        public string OwnerId { get; set; }

        [Display(Name = "Vendor (Seller)")]
        public virtual Owner Owner { get; set; }






        [Display(Name = "Customer(Buyer)")]
        public string CustomerId { get; set; }


        [Display(Name = "Customer")]
        public virtual Customer Customer { get; set; }





        //[Display(Name = "Delivery By")]
        //public string DeliverymanId { get; set; }

        //[Display(Name = "Delivery By")]
        //public virtual Deliveryman Deliveryman { get; set; }





        [Display(Name = "Bill To")]
        public string AddressBillToId { get; set; }

        [Display(Name = "Bill To")]
        public virtual AddressMain AddressBillTo { get; set; }

        [NotMapped]
        [Display(Name = "Bill To Address")]
        public virtual AddressComplex AddressBillToComplex { get; set; }

        [NotMapped]
        public SelectList SelectListAddressBillTo { get; set; }






        [Display(Name = "Ship To")]
        public string AddressShipToId { get; set; }

        [Display(Name = "Ship To")]
        public virtual AddressMain AddressShipTo { get; set; }

        [NotMapped]
        public SelectList SelectListAddressShipTo { get; set; }

        [NotMapped]
        [Display(Name = "Ship To")]
        public virtual AddressComplex AddressShipToComplex { get; set; }





        [NotMapped]
        public SelectList SelectListOwner { get; set; }

        [NotMapped]
        public SelectList SelectListCustomer { get; set; }

        [NotMapped]
        public SelectList SelectListDeliveryman { get; set; }
        [NotMapped]
        public SelectList SelectListAddressInformTo { get; set; }





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


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            DocumentAbstract docAb = icommonWithId as DocumentAbstract;
            docAb.IsNullThrowException();
            //DocumentNumber = docAb.DocumentNumber;
            //OwnerId = docAb.OwnerId;
            //CustomerId = docAb.CustomerId;
            AddressBillToId = docAb.AddressBillToId;
            AddressShipToId = docAb.AddressShipToId;
            AddressShipToComplex = docAb.AddressShipToComplex;
            AddressBillToComplex = docAb.AddressBillToComplex;

            PoNumber = docAb.PoNumber;
            PoDate = docAb.PoDate;
        }

    }
}
