using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    /// <summary>
    /// Owner is owner who has privilages to:
    ///     Invoice
    ///     Receive Payments against Invoice
    ///     Etc
    ///     Owner = VENDOR
    /// </summary>
    public class Owner : PlayerAbstract, IPlayer
    {


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.Owner;
        }



        [Display(Name = "Owner Category")]
        [MaxLength(128)]
        public virtual string OwnerCategoryId { get; set; }
        [Display(Name = "Owner Category")]
        public virtual OwnerCategory OwnerCategory { get; set; }


        [Display(Name = "Shops")]
        [InverseProperty("Owner")]
        public virtual ICollection<Product> Shops { get; set; }

        [NotMapped]
        public virtual List<Product> Shops_Fixed
        {
            get
            {
                if (Shops.IsNullOrEmpty())
                    return null;

                List<Product> undeletedShops = new List<Product>();
                foreach (Product shop in Shops)
                {
                    if (shop.IsShopExpired)
                        continue;
                    undeletedShops.Add(shop);
                }

                return undeletedShops;

            }
        }

        //public virtual ICollection<Product> Shops { get; set; }


        //[Display(Name = "Default Ship Address")]
        //public virtual string AddressDefaultShipFromId { get; set; }
        //public virtual AddressMain AddressDefaultShipFrom { get; set; }


        [NotMapped]
        public SelectList SelectListOwnerCategory { get; set; }


        //[Display(Name = "Salesman")]
        //public string SalesmanId { get; set; }
        //public virtual Salesman Salesman { get; set; }

        [NotMapped]
        public SelectList SelectListSalesman { get; set; }

        //[NotMapped]
        //public SelectList SelectListBillAddress { get; set; }


        //[NotMapped]
        //public SelectList SelectListShipAddress { get; set; }

        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);
            Owner owner = ic as Owner;
            owner.IsNullThrowException("Unable to unbox owner");
            OwnerCategoryId = owner.OwnerCategoryId;
            //AddressDefaultShipFromId = owner.AddressDefaultShipFromId;
        }
    }
}