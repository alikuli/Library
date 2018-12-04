using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Collections.Generic;

namespace UowLibrary.PlayersNS.PersonNS
{
    public partial class PersonBiz
    {





        public List<PersonUserFlatFile> FlattenPersonUser(List<Person> listPerson)
        {
            if (listPerson.IsNullOrEmpty())
                return null;
            List<PersonUserFlatFile> listPersonUserFlatFile = new List<PersonUserFlatFile>();
            foreach (var person in listPerson)
            {
                if (person.Users.IsNullOrEmpty())
                    continue;

                foreach (var userInPerson in person.Users)
                {
                    PersonUserFlatFile pu = new PersonUserFlatFile();
                    pu.PersonId = person.Id;
                    pu.UserId = userInPerson.Id;
                    listPersonUserFlatFile.Add(pu);

                }
            }

            return listPersonUserFlatFile;
        }





    }
}
