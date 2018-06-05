using InterfacesLibrary.SharedNS;
using System.Collections.Generic;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{


    public partial class Country : CountryAbstract, ICommonWithId
    {

        public Country()
        {
            //Discounts = new List<Discount>();
            States = new List<State>();
        }
        //public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<State> States { get; set; }


        public ICollection<ApplicationUser> User { get; set; }

        public override string ClassNamePlural
        {
            get
            {
                return "Countries";
            }
        }
    }
}