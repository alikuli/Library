using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.PlayersNS;
using UserModels;
using UserModelsLibrary.ModelsNS;
using WebLibrary.Programs;

namespace UowLibrary
{
    public partial class UserBiz : BusinessLayer<ApplicationUser>
    {

        ///// <summary>
        ///// Returns complete phone number
        ///// </summary>
        ///// <param name="oldPhoneNumber"></param>
        ///// <param name="countryAbbreviation"></param>
        ///// <returns></returns>
        //public string PhoneNumberFixer(string oldPhoneNumber, string countryAbbreviation)
        //{

        //    try
        //    {
        //        PhoneNumbersUtility phoneUtility = new PhoneNumbersUtility(oldPhoneNumber, countryAbbreviation);
        //        return phoneUtility.CompletePhoneNumber;

        //    }
        //    catch (Exception)
        //    {

        //        ErrorsGlobal.Add(string.Format("Unable to fix the following number: '{0}' for country abbreviation :'{1}'", oldPhoneNumber, countryAbbreviation), MethodBase.GetCurrentMethod());
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }

        //}


        public void Create(RegisterViewModel registerViewModel)
        {
            ApplicationUser u = Factory(registerViewModel);
            Create(u, registerViewModel.Password);
        }

        public bool Create(ApplicationUser newUser, string password)
        {

            try
            {
                IdentityResult result = UserManager.Create(newUser, password);
                return Result_Helper(newUser, result);
            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("User not created", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }


        }



        public async Task<bool> CreateAsync(ApplicationUser newUser, string password)
        {

            try
            {
                IdentityResult result = await UserManager.CreateAsync(newUser, password);
                return Result_Helper(newUser, result);
            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("User not created", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }

        }

        private bool Result_Helper(ApplicationUser newUser, IdentityResult result)
        {
            if (result == IdentityResult.Success)
            {
                ErrorsGlobal.AddMessage(string.Format("Successfully created user '{0}'", newUser.UserName));
                return true;
            }
            ErrorsGlobal.AddMessage(string.Format("user '{0}' not created", newUser.UserName));
            return false;
        }



        //public override void CreateSimple(ControllerCreateEditParameter parm)
        //{
        //    ApplicationUser appUser = parm.Entity as ApplicationUser;
        //    string generatedPassword = System.Web.Security.Membership.GeneratePassword(8, 2);
        //    Create(appUser, generatedPassword);
        //}
        //public override Create(ApplicationUser entity)
        //{
        //    string generatedPassword = System.Web.Security.Membership.GeneratePassword(8, 2);
        //    return Create(entity, generatedPassword);
        //}

        //public async Task CreateAsync(User entity)
        //{
        //    string generatedPassword = System.Web.Security.Membership.GeneratePassword(8, 2);
        //    await CreateAsync(entity, generatedPassword);
        //}






        //private void SendSmsToUser(string userId, string phoneNumber, string password, bool isAdmin)
        //{
        //    try
        //    {
        //        if (!isAdmin)
        //        {
        //            string addedText = string.Format("Password {0}", password);
        //            SendSmsCode(userId, phoneNumber, addedText);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Errors_UserDal.Add("SMS not sent!", MethodBase.GetCurrentMethod(), e);
        //        throw new Exception(Errors_UserDal.ToString());
        //    }
        //}





        ///// <summary>
        ///// Checks if user exists.
        ///// userName expected is unencrypted.
        ///// identityCardNo is unencrypted.
        ///// If user exists, returns user.
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="identityCardNo"></param>
        ///// <param name="isAdmin"></param>
        //private User GetUser(string userName, string identityCardNo, bool isAdmin)
        //{

        //    //check if user exists by name...
        //    User user = FindByUserName(userName);


        //    if (user != null)
        //        return user;

        //    //check if user exists by National Id Card
        //    if (!isAdmin)
        //        user = FindForIdentityCard(identityCardNo);

        //    return user;

        //}

        //private bool IsUserExist(string userName, string identityCardNo, bool isAdmin)
        //{
        //    User user = GetUser(userName, identityCardNo, isAdmin);

        //    if (user == null)
        //        return false;
        //    else
        //        return true;
        //}

        public ApplicationUser CreateUser_UserManager(string userName, string password)
        {
            throwErrorUserIsNullOrWhiteSpace(userName);


            //ApplicationUser appUser = UserManager.Find(userName, password);
            ApplicationUser appUser = FindByName(userName);
            if (!appUser.IsNull())
                return appUser;

            appUser = createUserEngine(userName, password);
            return appUser;

        }

        //This is 
        private ApplicationUser createUserEngine(string userName, string password)
        {
            ApplicationUser appUser = new ApplicationUser();
            appUser.UserName = userName;
            appUser.Name = userName;
            
            try
            {
                IdentityResult result = UserManager.Create(appUser, password);

                if (result.Succeeded)
                {
                    return appUser;
                }
                else
                {
                    if (!result.Errors.IsNull())
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var item in result.Errors)
                            {
                                ErrorsGlobal.Add(item, "Creating User");
                            }
                        }
                        else
                        {
                            ErrorsGlobal.Add("Errors are 0 but no user created.", "Creating User");

                        }
                    }
                    else
                    {
                        ErrorsGlobal.Add("Errors are null, but no user created", "Creating User");
                    }
                }

                throw new Exception();

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Unable to create user.", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());

            }
        }




    }
}
