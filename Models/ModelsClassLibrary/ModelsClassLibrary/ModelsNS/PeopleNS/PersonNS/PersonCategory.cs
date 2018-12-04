using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    public class PersonCategory : CommonWithId
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.PersonCategory;
        }


        public virtual ICollection<Person> Persons { get; set; }

    }
}