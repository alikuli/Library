using AliKuli.Extentions;
using UserModels;

namespace UowLibrary
{
    public partial class UserBiz : BusinessLayer<ApplicationUser>
    {





        //public void UpdateAndSaveDefaultAddress(string userId, string addressId)
        //{
        //    userId.IsNullOrWhiteSpaceThrowArgumentException();
        //    addressId.IsNullOrWhiteSpaceThrowArgumentException();

        //    ApplicationUser appUser = Find(userId);
        //    appUser.IsNullThrowException();
        //    appUser.PersonId.IsNullOrWhiteSpaceThrowArgumentException("No person for user");
        //    string personId = appUser.PersonId;

        //    PersonBiz.UpdateDefaultAddress(personId, addressId);
        //}

    }
}
