using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UserModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser, ICommonWithId
    {

        public void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {

            ApplicationUser u = (ApplicationUser)icommonWithId;

            MetaData.Modified.DateStart = u.MetaData.Modified.DateStart;

            DetailInfoToDisplayOnWebsite = u.DetailInfoToDisplayOnWebsite;
            Name = icommonWithId.Name;
            ReturnUrl = u.ReturnUrl;
            Comment = u.Comment;
            PersonId = u.PersonId;
            //CountryId = u.CountryId;
            //PhoneNumberAsEntered = u.PhoneNumberAsEntered;
            //AddressComplex = u.AddressComplex;
            //PersonComplex = u.PersonComplex;

            //IsActive = u.IsActive;
            SuspendedOldValue = Suspended.Value;
            BlackListOldValue = BlackListed.Value;

            Suspended.Value = u.Suspended.Value;
            BlackListed.Value = u.BlackListed.Value;

            //PhoneNumber = u.PhoneNumber;
            //CountryIdCardNumber = u.CountryIdCardNumber;



        }


        public ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.ApplicationUser;
        }


        public int ClassNameForRightsVal()
        {
            return (int)ClassNameForRights();
        }


        public bool IsAllowNameToBeSentanceCased
        {
            get { return true; }
        }


        public bool HideNameInView()
        {
            return true;
        }
    }
}
