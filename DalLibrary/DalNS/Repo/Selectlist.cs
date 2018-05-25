using AliKuli.Extentions;
using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Linq;
using System.Web.Mvc;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        #region SelectList
        ///// <summary>
        ///// This creates the data for the Selectlist. You can override this and create any kind of data you want
        ///// and it will be injected into the selectlist.
        ///// </summary>
        ///// <returns></returns>
        //public virtual IQueryable<TEntity> MakeDataForSelectList(TEntity entity)
        //{
        //    //return this.FindAll().AsQueryable();
        //    throw new NotImplementedException("Repository.MakeDataForSelectList(T entity)");
        //}


        /// <summary>
        /// Change the data by overriding MakeDataForSelectList(). ID can be changed by overriding IdString in the entity. Text can be changed by overriding FullName() in the entity.
        /// </summary>
        /// <returns></returns>
        public virtual SelectList SelectList()
        {
            if (this.IsNull())
                return null;

            var allItems = this.FindAll();

            if (allItems.IsNull() || allItems.Count() == 0)
                return null;

            var sortedList = allItems.OrderBy(x => x.Name)
                .Select(x =>
                new
                {
                    Text = x.Name,
                    Value = x.Id
                })
                .ToList();
            var selectList = new SelectList(sortedList, "Value", "Text");
            return selectList;
        }


        #endregion



    }
}
