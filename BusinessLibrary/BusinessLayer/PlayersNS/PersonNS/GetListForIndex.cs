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


            //first get a list of all the people
            List<Person> peopleList = await FindAllAsync();
            List<Person> peopleFoundLst = ListOfPeopleForUser(UserId, peopleList);

            if (peopleFoundLst.IsNullOrEmpty())
                return null;
            //cast and return the list
            var lstIcommonwithId = peopleFoundLst.Cast<ICommonWithId>().ToList();
            return lstIcommonwithId;



        }





    }
}
