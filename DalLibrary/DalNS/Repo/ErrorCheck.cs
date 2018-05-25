using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using System;
using System.Reflection;


namespace DalLibrary.DalNS
{

    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class,  ICommonWithId
    {

        #region Error Check
        /// <summary>
        /// The default Error Check checks to make sure no duplicate name is entered. If you want to allow a dupliate name to enter
        /// then make IsDuplicateNameAllowed to true;
        ///IsDeleting - When Deleting, this boolean is true.
        ///This collects all the errors and then throws the exception if there are errors. Therefore, always get the base last
        ///<para>SelfErrorCheck</para>
        ///<para>Check_ForDuplicate_Name</para>
        /// </summary>
        /// <param name="entity"></param>
        public virtual void ErrorCheck(TEntity entity)
        {
            entity.SelfErrorCheck();

            if (IsCreating)
                if (!IsDuplicateNameAllowed)
                {
                    Check_ForDuplicate_Name(entity);
                }

            if (ErrorsGlobal.HasErrors)
                throw new Exception(ErrorsGlobal.ToString());

        }

        private void Check_ForDuplicate_Name(TEntity entity)
        {
            TEntity entityInDb = FindForName(entity.Name);

            if (entityInDb != null)
            {
                string error = string.Format("Name: '{0}' already exists.", entity.Name);

                ErrorsGlobal.Add(error, MethodBase.GetCurrentMethod());
                //string errMsg = ErrorsGlobal.ToString();
                //ErrorsGlobal.Errors.Clear(); //This is an optional Error so it is cleared here

                throw new NoDuplicateException();
            }
        }

        #endregion

    }
}
