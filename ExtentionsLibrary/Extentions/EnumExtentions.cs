using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace AliKuli.Extentions
{
    public static class EnumExtention
    {
        /// <summary>
        /// To use:
        ///    MenuPath1ENUM mp1 = MenuPath1ENUM.Unknown;
        ///    var result2 = EnumExtention.ToSelectListSorted<MenuPath1ENUM>(mp1);

        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>


        public static System.Web.Mvc.SelectList ToSelectListSorted<TEnum>(this TEnum obj)
        where TEnum : struct,  IConvertible
        {

            var unsortedList = Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x =>
                    new SelectListItem
                    {
                        Text = Enum.GetName(typeof(TEnum), x),
                        Value = (Convert.ToInt32(x)).ToString()
                    });

            var sortedList = unsortedList.OrderBy(x => x.Text);

            return new SelectList(sortedList, "Value", "Text");

            //return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
            //    .Select(x =>
            //        new SelectListItem
            //        {
            //            Text = Enum.GetName(typeof(TEnum), x),
            //            Value = (Convert.ToInt32(x)).ToString()
            //        }), "Value", "Text");
        }

        public  static class Enum<T> where T : struct, IConvertible
        {
            public static int Count
            {
                get
                {
                    if (!typeof(T).IsEnum)
                        throw new ArgumentException("T must be an enumerated type");

                    return Enum.GetNames(typeof(T)).Length;
                }
            }
        }
        /// <summary>
        /// This creates a list for the enum and returns the words broken in Sentance Case i.e. Each word is capitalized
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>




        //#region enumToTitleSentanceList

        //public static List<string> ToList()
        //{
        //    if (!typeof(T).IsEnum)
        //        throw new ArgumentException("typeOfEnum must be an ENUM type");

        //    //Type underlyingType = Enum..GetUnderlyingType(typeof(T));

        //    var theList = Enum.GetNames(typeof(T)).Select(x => x.ToSentence()).OrderBy(x => x)
        //        .ToList();
        //    return theList;
        //}

        //#endregion

    }




    //public static class EnumExtentions
    //{
    //    /// <summary>
    //    /// This creates a list for the enum and returns the words broken in Sentance Case i.e. Each word is capitalized
    //    /// </summary>
    //    /// <param name="val"></param>
    //    /// <returns></returns>


    //    #region enumToTitleSentanceList
    //    public static List<string> ToListEnum(this Type enumType)
    //    {
    //        if (!theEnum.GetType().IsEnum)
    //            throw new ArgumentException("T must be an enumerated type");

    //        var theArray = Enum.GetNames(theEnum.GetType());
    //        string[] a = theArray.ToArray();

    //        return a.ToList();
    //    }
    //public static List<string> ToListEnum(this Type enumType)
    //{
    //    if (!theEnum.GetType().IsEnum)
    //        throw new ArgumentException("T must be an enumerated type");

    //    var theArray = Enum.GetNames(theEnum.GetType());
    //    string[] a = theArray.ToArray();

    //    return a.ToList();
    //}

    //public static List<string> ToList(this EnumType enumType, string actualEnum)
    //{

    //    if (!actualEnum.GetType().IsEnum)
    //        throw new ArgumentException("This must be an enumerated type");

    //    //get the underlying cast

    //    Type underlyingType = Enum.GetUnderlyingType(actualEnum.GetType());

    //    var lst = Enum.GetNames(underlyingType).ToList();

    //    return lst;
    //}


    //#endregion



}