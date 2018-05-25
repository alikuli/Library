using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InterfacesLibrary.DiscountNS;
using InterfacesLibrary.GeoLocationNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ModelsNS.GeoLocationNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;


namespace ModelsClassLibrary.ModelsNS.PlayersNS
{
    /// <summary>
    /// Note. The CustomerVendorAbstract is connected to the User. Therefore, this customer is tied up with the login ID.
    /// </summary>
    public class Customer : PlayerAbstract, ICustomer
    {
        #region CustomerCategory

        [Display(Name = "Category")]
        public Guid CustomerCategoryId { get; set; }

        public virtual ICategory CustomerCategory { get; set; }

        #endregion


        #region Navigation

        public virtual ICollection<IGeoLocation> ListOfGeoLocationsToWork { get; set; }
        public virtual ICollection<IDiscount> CustomerDiscounts { get; set; }

        #endregion

        #region SelfErrorCheck
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

            Check_CustomerCategory();


        }

        private void Check_CustomerCategory()
        {
            if (CustomerCategory == null)
                throw new Exception("Customer Category is null. Customer.Check_CustomerCategory");
            if (CustomerCategory == null)
                throw new Exception("Customer CategoryId is null. Customer.Check_CustomerCategory");
        }

        #endregion

        public void LoadFrom(ICustomer c)
        {
            base.LoadFrom(c as PlayerAbstract);
            CustomerCategory = (CustomerCategory) c.CustomerCategory;
            CustomerCategoryId = ((Customer) c).CustomerCategoryId;
        }


    }
}