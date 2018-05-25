using System;
using System.Reflection;
using System.Web.Mvc;
using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary.Abstract;
using UowLibrary.Interface;
using WebLibrary.Programs;
using InterfacesLibrary.SharedNS;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {



        /// <summary>
        /// This loads the Language Data into the select list
        /// </summary>
        /// <returns></returns>
        public virtual SelectList SelectList()
        {
            SelectList selectList = getSelectListFromCache();
            if (selectList.IsNull())
            {
                selectList = Dal.SelectList();
                storeIntoCache(selectList);
            }
            return selectList;
        }

        public SelectList UsersSelectList()
        {
            SelectList selectList = getSelectListFromCache();
            if (selectList.IsNull())
            {
                selectList = UserDal.SelectList();
                storeIntoCache(selectList);
            }
            return selectList;

        }

        public abstract string SelectListCacheKey { get; }

        private SelectList getSelectListFromCache()
        {

            object obj = MemoryMain.CacheMemory.GetFrom(SelectListCacheKey);
            ErrorsGlobal.AddMessage("Select List Cache Accessed.", MethodBase.GetCurrentMethod());
            return (SelectList)obj;
        }

        public void ClearSelectListInCache(string locationName)
        {
            MemoryMain.CacheMemory.ClearFor(locationName);
        }

        private void storeIntoCache(SelectList selectList)
        {
            MemoryMain.CacheMemory.Add(SelectListCacheKey, selectList);
        }


        public dynamic SelectListJson()
        {
            throw new NotImplementedException();
        }

    }
}
