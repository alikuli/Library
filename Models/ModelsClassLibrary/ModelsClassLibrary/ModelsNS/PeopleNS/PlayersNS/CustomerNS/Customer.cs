using EnumLibrary.EnumNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using UserModels;
using AliKuli.Extentions;


namespace ModelsClassLibrary.ModelsNS.PlayersNS
{
    /// <summary>
    /// Note. The CustomerVendorAbstract is connected to the User. Therefore, this customer is tied up with the login ID.
    /// </summary>
    public class Customer : PlayerAbstract, ICustomer, IPlayer
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.Customer;
        }



        [Display(Name = "Category")]
        [MaxLength(128)]
        public string CustomerCategoryId { get; set; }

        public virtual ICategory CustomerCategory { get; set; }

        


        //[Display(Name = "Bill To")]
        //public virtual string DefaultBillAddressId { get;set; }
        //public virtual AddressWithId DefaultBillAddress { get; set; }



        
        [Display(Name="Inform To")]
        [MaxLength(128)]
        public virtual string DefaultInformToAddressId { get; set; }
        public virtual AddressMain DefaultInformToAddress { get; set; }

        
        
        [NotMapped]
        public SelectList SelectListCustomerCategory { get; set; }


        [NotMapped]
        public SelectList SelectListAddressBillTo { get; set; }


        [NotMapped]
        public SelectList SelectListAddressShipTo { get; set; }


        [NotMapped]
        public SelectList SelectListAddressInformTo { get; set; }



        public override void UpdatePropertiesDuringModify(ICommonWithId ic)
        {
            Customer customer = ic as Customer;
            customer.IsNullThrowException("Unable to unbox customer");

            CustomerCategoryId = customer.CustomerCategoryId;
            DefaultBillAddressId = customer.DefaultBillAddressId;
            DefaultShipAddressId = customer.DefaultShipAddressId;
            DefaultInformToAddressId = customer.DefaultInformToAddressId;

            base.UpdatePropertiesDuringModify(ic);
        }


        //private void LoadFrom(ICustomer c)
        //{
        //    CustomerCategory = (CustomerCategory) c.CustomerCategory;
        //    CustomerCategoryId = ((Customer) c).CustomerCategoryId;
        //}




        //public new void LoadFrom(ICustomer c)
        //{
        //    throw new NotImplementedException();
        //}
        //public virtual ICollection<IGeoLocation> ListOfGeoLocationsToWork { get; set; }
        //public virtual ICollection<IDiscount> CustomerDiscounts { get; set; }


        //public override void SelfErrorCheck()
        //{
        //    base.SelfErrorCheck();
        //    Check_CustomerCategory();


        //}

        //private void Check_CustomerCategory()
        //{
        //    if (CustomerCategory == null)
        //        throw new Exception("Customer Category is null. Customer.Check_CustomerCategory");
        //    if (CustomerCategory == null)
        //        throw new Exception("Customer CategoryId is null. Customer.Check_CustomerCategory");
        //}
    }
}