using EnumLibrary.EnumNS;
using InterfacesLibrary.AddressNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    public class AddressWithId : CommonWithId, IAddressWithId
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return ClassesWithRightsENUM.Address;
        }
        public AddressWithId()
        {
            //default is true for all address types
            AddressType = new AddressType();
            AddressComplex = new AddressComplex();
        }
        /// <summary>
        /// AddressComplex is basically IAddressString.
        /// </summary>
        public AddressComplex AddressComplex { get; set; }

        #region User

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }


        #endregion


        #region Country

        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        #endregion


        /// <summary>
        /// If general, then none is selected.
        /// </summary>
        [Display(Name = "Type")]
        public AddressType AddressType { get; set; }


        public override void UpdatePropertiesDuringModify(ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);

            AddressWithId a = ic as AddressWithId;

            if (a == null)
            {
                throw new Exception("Unable to box AddressWithId. AddressWithId.UpdatePropertiesDuringModify");
            }


            AddressComplex = a.AddressComplex;
            AddressType = a.AddressType;
            UserId = a.UserId;
            CountryId = a.CountryId;

        }



    }
}