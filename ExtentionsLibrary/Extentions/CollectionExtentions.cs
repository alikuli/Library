using System.Collections.Generic;
using System.Linq;

namespace AliKuli.Extentions
{
    public static class CollectionExtentions
    {

        //public static bool IsNullOrEmpty(this ICollection e)
        //{
        //    return e.IsNull() || e.Count == 0;
        //}

        public static bool IsNullOrEmpty<T>(this ICollection<T> e)
        {
            return e.IsNull() || e.Count() == 0;
        }
        //public static bool IsNullOrEmpty(this Collection<object> e)
        //{
        //    return e.IsNull() || e.Count() == 0;
        //}

        public static bool IsNullOrEmpty<T>(this IQueryable<T> e)
        {
            return e.IsNull() || e.ToList<T>().Count() == 0;
        }


        //public static bool IsNullOrEmpty(this object[] e)
        //{
        //    return e.IsNull() || e.ToList<object>().Count() == 0;
        //}

        //public static List<object> ToList(this object[] objs)
        //{
        //    List<object> objList = new List<object>();


        //    if (!objs.IsNullOrEmpty())
        //    {
        //        foreach (var item in objs)
        //        {
        //            objList.Add(item);
        //        }
        //    }
        //    return objList;
        //}

        //public static bool IsNullOrEmpty(this Collection<object> e)
        //{
        //    return e.IsNull() || e.Count() == 0;
        //}

        //public static bool IsNullOrEmpty(this Collection<string> e)
        //{
        //    return e.IsNull() || e.Count() == 0;
        //}

    }
}