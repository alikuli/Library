using System;
using System.Collections.Generic;
using System.Linq;


namespace AliKuli.Extentions
{
    public static class EnumExtention<T> where T: struct, IConvertible
    {
        /// <summary>
        /// This creates a list for the enum and returns the words broken in Sentance Case i.e. Each word is capitalized
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>


        #region enumToTitleSentanceList
        
        public static List<string> ToList()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("typeOfEnum must be an ENUM type");
            
            //Type underlyingType = Enum..GetUnderlyingType(typeof(T));

            var theList = Enum.GetNames(typeof(T)).Select(x => x.ToSentence()).OrderBy(x => x)
                .ToList();
            return theList;
        }

        #endregion

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