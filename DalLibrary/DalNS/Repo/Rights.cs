using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.RightsNS;
using System;
using System.Linq;
using System.Reflection;
using UserModels;

namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {



        private bool canCreate()
        {

            //check if user is Authourized
            IRight right = getUserRights();


            if (!right.Create)
            {
                //No Rights.
                string e = string.Format("User {0} Has no rights to Create, FAILED for {1}",
                    UserName,
                    typeof(TEntity).Name.ToTitleSentance());

                ErrorsGlobal.Add(e, "Create");
                throw new Exception(ErrorsGlobal.ToString());

            }
            return true;
        }


        private bool canRetrieve()
        {

            //check if user is Authourized
            IRight right = getUserRights();


            if (!right.Retrieve)
            {
                //No Rights.
                string e = string.Format("User {0} Has no rights to Retrieve, FAILED for {1}",
                    UserName,
                    typeof(TEntity).Name.ToTitleSentance());

                ErrorsGlobal.Add(e, "Retrieve");
                throw new Exception(ErrorsGlobal.ToString());

            }
            return true;


        }


        private bool canUpdate()
        {
            //ifNotLoggedInThrowError();

            //check if user is Authourized
            IRight right = getUserRights();


            if (!right.Update)
            {

                //No Rights.
                string e = string.Format("User {0} Has no rights to Update, FAILED for {1}",
                    UserName,
                    typeof(TEntity).Name.ToTitleSentance());

                ErrorsGlobal.Add(e, "Update");
                throw new Exception(ErrorsGlobal.ToString());
            }
            return true;
        }



        private bool canDelete()
        {

            //check if user is Authourized
            IRight right = getUserRights();


            if (!right.Delete)
            {
                //No Rights.
                string e = string.Format("User {0} Has no rights to Delete, FAILED for {1}",
                    UserName,
                    typeof(TEntity).Name.ToTitleSentance());

                ErrorsGlobal.Add(e, "Delete");
                throw new Exception(ErrorsGlobal.ToString());

            }
            return true;
        }


        private void canDeleteActually()
        {
            //ifNotLoggedInThrowError();

            //check if user is Authourized
            IRight right = getUserRights();


            if (!right.DeleteActually)
            {
                //No Rights.
                string e = string.Format("User {0} Has no rights to Delete, FAILED for {1}",
                    UserName,
                    typeof(TEntity).Name.ToTitleSentance());

                ErrorsGlobal.Add(e, "Delete");
                throw new Exception(ErrorsGlobal.ToString());

            }
        }


        /// <summary>
        /// Gets user's rights.
        /// </summary>
        /// <returns></returns>
        private IRight getUserRights()
        {
            TEntity entityDud = Factory();

            if (!isUserLoggedIn())
            {
                return getDefaultRights(entityDud.ClassNameForRights());
            }

            if (isUserAdmin())
            {
                return fullRights(entityDud);
            }

            //if user is logged in.
            //get the user record.
            //Get the rights for the user.

            ApplicationUser user = _db.Users.FirstOrDefault(x => x.Id == UserId);

            if (user.IsNull())
            {
                ErrorsGlobal.Add("No user found.", MethodBase.GetCurrentMethod());
                return getDefaultRights(entityDud.ClassNameForRights());
            }

            //This can be any kind of class
            //we convert it to Right so that we can get the method MakeKey
            //but this will only work when the admin is making rights....
            //hopw will it work when rights are being determined for the user?

            Right rightDud = new Right();
            rightDud.RightsFor = entityDud.ClassNameForRights();
            rightDud.User = user;
            rightDud.UserId = user.Id;

            string userRightId = rightDud.MakeKey();

            var userRightsList = _db.Rights.Where(x => !x.MetaData.IsDeleted).ToList();
            Right userRight = userRightsList.FirstOrDefault(x => x.Id == userRightId);

            if (userRight.IsNull())
            {
                string e = string.Format("User {0} has no right for {1}!", user.UserName, entityDud.ClassNameForRights());
                ErrorsGlobal.Add(e, "Get User Right");
                throw new Exception(ErrorsGlobal.ToString());

            }

            return userRight;
        }


        /// <summary>
        /// Checks to see if user is logged in. If UserId has no ID, then it is assumed user is not logged in.
        /// </summary>
        /// <returns></returns>
        private bool isUserLoggedIn()
        {
            return !UserId.IsNullOrWhiteSpace();
        }



        //private IRight defaultRights(TEntity dudForRights)
        //{
        //    IRight r = getDefaultRights(dudForRights.ClassNameForRights());
        //    return r;
        //}

        /// <summary>
        /// Gives user full rights.
        /// </summary>
        /// <param name="dudForRights"></param>
        /// <returns></returns>
        private static IRight fullRights(TEntity dudForRights)
        {
            return (IRight)new CRUD_Actually_Right(dudForRights.ClassNameForRights());
        }


        /// <summary>
        /// Checks to see if user is Admin
        /// </summary>
        /// <returns></returns>
        private bool isUserAdmin()
        {

            if (!isUserLoggedIn())
                return false;

            ApplicationUser user = _db.Users.Find(UserId);
            if (user.IsNull())
                return false;


            ConfigManagerHelper config = new ConfigManagerHelper();
            IdentityRole role = _db.Roles.FirstOrDefault(x => x.Name == config.AdminRole);

            bool roleNotFound = role.IsNull();
            if (roleNotFound)
                return false;

            IdentityUserRole ir = _db.UserRoles.FirstOrDefault(x => x.RoleId == role.Id && x.UserId == user.Id);

            bool isNotRoleForUser = ir.IsNull();
            if (isNotRoleForUser)
                return false;

            return true;
        }

        //private void ifNotLoggedFindDefaultRight()
        //{
        //    //Check if logged in...
        //    if (UserId.IsNullOrWhiteSpace())
        //    {


        //        //Not logged in.

        //        getDefaultRight();


        //        //string e = string.Format("User Not Logged In.  Please log in.");
        //        //ErrorsGlobal.Add(e, "Right");
        //        //throw new Exception(ErrorsGlobal.ToString());
        //    }
        //}



    }
}
