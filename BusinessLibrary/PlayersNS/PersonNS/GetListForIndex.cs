using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UserModels;

namespace UowLibrary.PlayersNS.PersonNS
{
    public partial class PersonBiz
    {


        public override IList<ICommonWithId> GetListForIndex()
        {
            throw new NotImplementedException();

            //try
            //{
            //    UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");


            //    //get user for the person.
            //    ApplicationUser user = UserBiz.FindAll().FirstOrDefault(x => x.Id == UserId);
            //    if (user.IsNull())
            //        return null;
                
            //    if (user.PersonId.IsNullOrWhiteSpace())
            //        return null;

            //    //then get the person from the User.
            //    Person person = Find(user.PersonId);

            //    if (person.IsNull())
            //        return null;





            //    List<Person> personList = new List<Person>();
            //    personList.Add(person);

            //    var lstIcommonwithId = personList.Cast<ICommonWithId>().ToList();
            //    return lstIcommonwithId;
            //}
            //catch (Exception e)
            //{
            //    ErrorsGlobal.Add("Unable to continue", MethodBase.GetCurrentMethod(), e);
            //    throw new Exception(ErrorsGlobal.ToString());
            //}
        }


        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {
            //throw new NotImplementedException();
            try
            {
                //first get a list of all the people
                List<Person> people = await FindAllAsync();
                if(people.IsNullOrEmpty())
                    return null;

                //flatten the list of people and users
                List<PersonUserFlatFile> personUserFlatFile = FlattenPersonUser(people);

                if (personUserFlatFile.IsNull())
                    return null;


                //locate person from the flat file user the User's Id
                List<PersonUserFlatFile> peopleFoundFlatFile = personUserFlatFile.Where(x => x.UserId == UserId).ToList();

                if (peopleFoundFlatFile.IsNullOrEmpty())
                    return null;

                //No list only contains those users that have this person. There should always be one or none.

                List<Person> peopleFoundLst = new List<Person>();
                //Add the list of people found into a list
                foreach (PersonUserFlatFile personUserIds in peopleFoundFlatFile)
                {
                    Person personForUser = people.FirstOrDefault(x => x.Id == personUserIds.PersonId);
                    personForUser.IsNullThrowException("Person not found. Programming error.");
                    peopleFoundLst.Add(personForUser);
                }


                //cast and return the list
                var lstIcommonwithId = peopleFoundLst.Cast<ICommonWithId>().ToList();
                return lstIcommonwithId;


            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Unable to continue", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }

        }





    }
}
