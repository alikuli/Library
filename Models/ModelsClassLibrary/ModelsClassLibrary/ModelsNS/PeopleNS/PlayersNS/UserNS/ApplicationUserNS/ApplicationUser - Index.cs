using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.UserNameSpace;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

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
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY, ClassNameRaw);
        }

    }
}
