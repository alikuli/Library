using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PlacesNS.PhoneNS
{
    public class Phone : PhoneEmailAddressAbstract
    {
        //[Phone]
        //[Required]
        //[NotMapped]
        //public string PhoneNo { get; set; }
        //public string PersonId { get; set; }
        //public Person Person { get; set; }


        [Display(Name = "Country")]
        public string CountryId { get; set; }
        public Country Country { get; set; }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Phone;
        }

        [NotMapped]
        public SelectList SelectListCountry { get; set; }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            Phone phone = icommonWithId as Phone;
            phone.IsNullThrowException();
            //Name = phone.PhoneNo;
            CountryId = phone.CountryId;


        }
    }
}
