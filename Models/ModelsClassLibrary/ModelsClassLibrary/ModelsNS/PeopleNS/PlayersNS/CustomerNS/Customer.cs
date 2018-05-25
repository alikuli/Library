using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InterfacesLibrary.GeoLocationNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.GeoLocationNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using EnumLibrary.EnumNS;


namespace ModelsClassLibrary.ModelsNS.PlayersNS
{
    /// <summary>
    /// Note. The CustomerVendorAbstract is connected to the User. Therefore, this customer is tied up with the login ID.
    /// </summary>
    public class Customer : PlayerAbstract, ICustomer
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return EnumLibrary.EnumNS.ClassesWithRightsENUM.Customer;
        }
        #region CustomerCategory

        [Display(Name = "Category")]
        public Guid CustomerCategoryId { get; set; }

        public virtual ICategory CustomerCategory { get; set; }

        #endregion


        #region Navigation

        //public virtual ICollection<IGeoLocation> ListOfGeoLocationsToWork { get; set; }
        //public virtual ICollection<IDiscount> CustomerDiscounts { get; set; }

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

        public override void UpdatePropertiesDuringModify(ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);
        }
        private void LoadFrom(ICustomer c)
        {
            CustomerCategory = (CustomerCategory) c.CustomerCategory;
            CustomerCategoryId = ((Customer) c).CustomerCategoryId;
        }




        //public new void LoadFrom(ICustomer c)
        //{
        //    throw new NotImplementedException();
        //}
    }
}