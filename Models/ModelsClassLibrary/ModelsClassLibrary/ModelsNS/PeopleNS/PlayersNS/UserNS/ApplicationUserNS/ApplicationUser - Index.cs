using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser, ICommonWithId
    {

        public string Input1SortString { get { return UserName; } }


        public string Input2SortString { get { return PhoneNumber; } }


        public string Input3SortString { get; set; }




        public string NameInput1 { get { return "UserName"; } }


        public string NameInput2 { get { return "Phone Number"; } }

        public string NameInput3 { get { return ""; } }



        public string ClassNameRaw
        {
            get { return "User"; }
        }

        public string ClassName
        {
            get { return ClassNameRaw; }
        }

        public string ClassNamePlural
        {
            get { return ClassName + "s"; }
        }

        public string MiscFilesLocation_Initialization()
        {
            return AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;

        }

    }
}
