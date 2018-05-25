using AliKuli.Extentions;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        #region Booleans
        /// <summary>
        /// Since we do not delete in actuality... this is checked in
        /// errorCheck in Updates... right before the update. If this is true,
        /// Then there are no error checks
        /// </summary>
        protected static bool IsDeleting { get; set; }

        /// <summary>
        /// If this is true, then creation is being done. Helps in error checking for duplicates
        /// </summary>
        protected static bool IsCreating { get; set; }

        protected static bool IsUpdating { get; set; }

        protected virtual bool IsExistName(string s)
        {

            if (string.IsNullOrWhiteSpace(s))
                return false;

            //check for name
            bool found = FindForName(s) != null ? true : false;

            return found;
        }


        protected static bool IsDuplicateNameAllowed { get; set; }
        #endregion





        public string UserId { get; set; }
    }
}

