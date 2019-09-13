using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser, ICommonWithId
    {

        public void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {

            ApplicationUser u = (ApplicationUser)icommonWithId;

            DetailInfoToDisplayOnWebsite = u.DetailInfoToDisplayOnWebsite;
            Name = icommonWithId.Name;
            Comment = u.Comment;
            PersonId = u.PersonId;
            SuspendedOldValue = Suspended.Value;
            BlackListOldValue = BlackListed.Value;
            Suspended.Value = u.Suspended.Value;
            BlackListed.Value = u.BlackListed.Value;


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
