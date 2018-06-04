using AliKuli.Extentions;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        #region Delete
        public virtual void Delete(string id)
        {
            if (id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("No id received for deletion. Programming error.", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            var entity = this.FindFor(id);

            if(entity.IsNull())
            {
                ErrorsGlobal.Add("Entity not found. Programming error.", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }
            this.Delete(entity);

        }


        public virtual async Task DeleteAsync(string id)
        {

            var entity = await this.FindForAsync(id);
            this.Delete(entity);

        }
        //--------------------------------------------------------------------------------------------


        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new NoNullAllowedException();

            canDelete();

            //We will never delete anything... we just make Delete True
            entity.MetaData.IsDeleted = true;
            entity.MetaData.Deleted.By = "";
            entity.MetaData.IsActive = false;

            //Initialize();
            IsDeleting = true; //there will be no error checks

            this.Update(entity);
        }

        public virtual void DeleteActually(TEntity entity)
        {
            if (entity == null)
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("Missing entity (T). Repository.DeleteActually (T)");

            canDeleteActually();

            _db.Set<TEntity>().Remove(entity);

        }
        public virtual void DeleteActually(string id)
        {
            if (id.IsNullOrWhiteSpace())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("Missing entity Id. Repository.DeleteActually (T)");

            var entity = FindFor(id);
            entity.IsNullThrowException("Entity not found!");
            DeleteActually(entity);
        }
        //--------------------------------------------------------------------------------------------



        //public virtual Task Delete(string id)
        //{
        //    if (id.IsNullOrEmpty())
        //        throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("Missing parmeter (id). Repository.DeleteAsync (id)");

        //    var item = this.FindFor(id);
        //    this.Delete(item);
        //}
        //--------------------------------------------------------------------------------------------

        #endregion

    }
}

