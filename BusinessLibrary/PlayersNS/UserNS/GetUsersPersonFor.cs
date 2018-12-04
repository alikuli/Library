using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UserModels;
//using AliKuli.Extentions;
using System.Linq;
namespace UowLibrary
{
    public partial class UserBiz 
    {

        public Person GetPersonFor(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowArgumentException("userId is null");
            ApplicationUser user = Find(userId);
            user.IsNullThrowException("User not found");

            if (user.Person.IsNull())
                return null;

            Person person = user.Person;
            person.IsNullThrowException("The user does not have a person even though there should be one");
            return person;


        }

        public bool HasPersonForUser(string userId)
        {
            return !GetPersonFor(userId).IsNull();
        }



        //public void AddPersonForUser(string userId)
        //{
        //    userId.IsNullOrWhiteSpaceThrowArgumentException("userId is null");
        //    if (HasPersonForUser(userId))
        //        return;
        //    Person person = PersonBiz.Factory() as Person;

        //    ApplicationUser user = Find(userId);
        //    user.IsNullThrowException("User not found");
        //    user.PersonId = person.Id;
        //    person.Users.Add(user);
            
        //    Update(user);
        //    PersonBiz.CreateAndSave(person);
        //}
    }
}
