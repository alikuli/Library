using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using InterfacesLibrary.DiscountNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
//using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ModelsNS.People;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{
    public class Vendor : PlayerAbstract, IVendor
    {
        #region Navigation
        //public ICollection<IDiscount> Discounts { get; set; }

        #endregion

        #region Category
        [Display(Name = "Category")]
        public long CategoryId { get; set; }
        public virtual ICategory Category { get; set; }

        #endregion

        public void LoadFrom(IVendor p)
        {
            Category = p.Category;
            CategoryId = p.CategoryId;
            //Discounts =  (ICollection<IDiscount>) p.Discounts;

            base.LoadFrom(p as PlayerAbstract);


        }
    }
}