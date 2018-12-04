using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddessWithIdNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{
    public class PhoneEmailAddressAbstract : CommonWithId, IAmVerified
    {
        public PhoneEmailAddressAbstract()
        {
            VerificationDateComplex = new DateAndByComplex();
            VerificationStatusEnum = VerificaionStatusENUM.NotVerified;
        }

        ///// <summary>
        ///// This is where the verification number is stored.
        ///// </summary>
        ///// 
        //[Required]
        public string PersonId { get; set; }
        public Person Person { get; set; }


        public virtual ICollection<Person> People { get; set; }

        [Display(Name = "Verification Number")]
        public string VerificationNumber { get; set; }


        [NotMapped]
        public SelectList SelectListAddressVerificationEnum { get; set; }
        //public bool IsVerified { get; set; }

        public DateAndByComplex VerificationDateComplex { get; set; }

        public VerificaionStatusENUM VerificationStatusEnum { get; set; }


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            PhoneEmailAddressAbstract pea = icommonWithId as PhoneEmailAddressAbstract;
            PersonId = pea.PersonId;
            VerificationStatusEnum = pea.VerificationStatusEnum;
            VerificationDateComplex = pea.VerificationDateComplex;
            //IsVerified = pea.IsVerified;
        }
    }
}
