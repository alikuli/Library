using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using System.Linq;
using System.Web.Mvc;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {


        #region SelectList
        public virtual SelectList SelectList()
        {

            var allItems = this.FindAll();
            return SelectList_Engine(allItems);
        }


        /// <summary>
        /// You can switch the data of the SelectList from here.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual SelectList SelectList_Engine(IQueryable<TEntity> data)
        {
            var allItems = data;

            //if (allItems.IsNull() || allItems.Count() == 0)
            //    return new SelectList(null, "Value", "Text");

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
