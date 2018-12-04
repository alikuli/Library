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
    /// Note. The DeliverymanVendorAbstract is connected to the User. Therefore, this deliveryman is tied up with the login ID.
    /// </summary>
    public class Deliveryman : PlayerAbstract
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.Deliveryman;
        }



        [Display(Name = "Category")]
        [MaxLength(128)]
        public string DeliverymanCategoryId { get; set; }

        public virtual ICategory DeliverymanCategory { get; set; }




        
        
        [NotMapped]
        public SelectList SelectListDeliverymanCategory { get; set; }


        [NotMapped]
        public SelectList SelectListAddressBillTo { get; set; }



        public override void UpdatePropertiesDuringModify(ICommonWithId ic)
        {
            Deliveryman deliveryman = ic as Deliveryman;
            deliveryman.IsNullThrowException("Unable to unbox deliveryman");

            DeliverymanCategoryId = deliveryman.DeliverymanCategoryId;
            DefaultBillAddressId = deliveryman.DefaultBillAddressId;
            //DefaultShipAddressId = deliveryman.DefaultShipAddressId;
            //DefaultInformToAddressId = deliveryman.DefaultInformToAddressId;

            base.UpdatePropertiesDuringModify(ic);
        }


        //private void LoadFrom(IDeliveryman c)
        //{
        //    DeliverymanCategory = (DeliverymanCategory) c.DeliverymanCategory;
        //    DeliverymanCategoryId = ((Deliveryman) c).DeliverymanCategoryId;
        //}




        //public new void LoadFrom(IDeliveryman c)
        //{
        //    throw new NotImplementedException();
        //}
        //public virtual ICollection<IGeoLocation> ListOfGeoLocationsToWork { get; set; }
        //public virtual ICollection<IDiscount> DeliverymanDiscounts { get; set; }


        //public override void SelfErrorCheck()
        //{
        //    base.SelfErrorCheck();
        //    Check_DeliverymanCategory();


        //}

        //private void Check_DeliverymanCategory()
        //{
        //    if (DeliverymanCategory == null)
        //        throw new Exception("Deliveryman Category is null. Deliveryman.Check_DeliverymanCategory");
        //    if (DeliverymanCategory == null)
        //        throw new Exception("Deliveryman CategoryId is null. Deliveryman.Check_DeliverymanCategory");
        //}
    }
}