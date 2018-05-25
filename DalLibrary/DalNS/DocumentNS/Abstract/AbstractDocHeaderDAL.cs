using System;
using AliKuli.Extentions;

using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS;

using UserModels.Models;


namespace DalLibrary.DalNS.DocumentNS
{
    /// <summary>
    /// This fixes the common items of T. Do not use this for adding, deleting and updating
    ///  <para>Fix_DocNo(entity);</para>
    ///  <para>Fix_Date(entity);</para>
    /// </summary>
    public class AbstractDocHeaderDAL<T> : Repositry<T> where T : AbstractDocHeader
    {
        public AbstractDocHeaderDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass("AbstractDocHeaderDAL");

        }





        public override void Fix(T entity)
        {
            base.Fix(entity);

            Fix_DocNo(entity);//only if creating
            Fix_Date(entity);

        }



        private void Fix_DocNo(T entity)
        {
            if (isCreating)
                if (entity.DocNo == -1)
                    entity.DocNo = GetNextDocNumber();
        }

        private void Fix_Date(T entity)
        {
            if (isCreating || isUpdating)
            {
                if (entity.Date.IsMinOrMax())
                {
                    entity.Date = DateTime.UtcNow;
                }
            }
        }



        #region Get...

        public virtual long GetNextDocNumber()
        {
            throw new NotImplementedException("");
        }



        #endregion




        /// <summary>
        /// <para>SelfErrorCheck</para>
        /// <para>Check_Customer_BlackListed</para>
        /// <para>Check_Owner_BlackListed</para>
        /// <para>Check_Salesman_BlackListed</para>
        /// </summary>
        /// <param name="entity"></param>
        public override void ErrorCheck(T entity)
        {
            base.ErrorCheck(entity);

        }



    }
}