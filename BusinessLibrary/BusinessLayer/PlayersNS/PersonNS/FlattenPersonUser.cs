using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
namespace UowLibrary.PlayersNS.PersonNS
{
    public partial class PersonBiz
    {




        ///// <summary>
        ///// This finds the list of persons with the supplied uerId. Normally, a full list of persons
        ///// needs to be supplied
        ///// </summary>
        ///// <param name="userId">The UserId we want</param>
        ///// <param name="people">The list of people, normally the full list</param>
        ///// <returns></returns>
        //public List<Person> ListOfPeopleForUser(string userId, List<Person> people)
        //{
        //    try
        //    {
        //        if (people.IsNullOrEmpty())
        //            return null;

        //        //flatten the list of people and users
        //        List<PersonUserFlatFile> personUserFlatFile = flattenPersonUser(people);

        //        if (personUserFlatFile.IsNull())
        //            return null;


        //        //locate person from the flat file user the User's Id
        //        List<PersonUserFlatFile> peopleFoundFlatFile = personUserFlatFile.Where(x => x.UserId == UserId).ToList();

        //        if (peopleFoundFlatFile.IsNullOrEmpty())
        //            return null;

        //        //Now list only contains those users that have this person. There should always be one or none.

        //        List<Person> found = new List<Person>();
        //        //Add the list of people found into a list
        //        foreach (PersonUserFlatFile personUserIds in peopleFoundFlatFile)
        //        {
        //            Person personForUser = people.FirstOrDefault(x => x.Id == personUserIds.PersonId);
        //            personForUser.IsNullThrowException("Person not found. Programming error.");
        //            found.Add(personForUser);
        //        }
        //        if (!found.IsNullOrEmpty())
        //            if (found.Count > 1)
        //            {
        //                string nameStr = ""; 
        //                foreach (var p in found)
        //                    nameStr += string.Format("{0}; ", p.Name);

        //                throw new Exception(string.Format("There are more than one person for this user! '{0}'", nameStr));
        //            }
        //        return found;
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Unable to continue", MethodBase.GetCurrentMethod(), e);
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }

        //}

        //private List<PersonUserFlatFile> flattenPersonUser(List<Person> listPerson)
        //{
        //    if (listPerson.IsNullOrEmpty())
        //        return null;

        //    List<PersonUserFlatFile> listPersonUserFlatFile = new List<PersonUserFlatFile>();
        //    foreach (var person in listPerson)
        //    {
        //        if (person.Users.IsNullOrEmpty())
        //            continue;

        //        foreach (var userInPerson in person.Users)
        //        {
        //            PersonUserFlatFile pu = new PersonUserFlatFile();
        //            pu.PersonId = person.Id;
        //            pu.UserId = userInPerson.Id;
        //            listPersonUserFlatFile.Add(pu);

        //        }
        //    }

        //    return listPersonUserFlatFile;
        //}





    }
}
