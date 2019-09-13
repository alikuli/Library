//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace AliKuli.Utilities
//{
//    public static class SaveRetrieveFromApplicationMemory
//    {
//        public static string Memory
//        {
//            get
//            {

//                if (HttpContext.Current == null)
//                    return "";

//                return HttpContext.Current.Application["Location1"] != null ? HttpContext.Current.Application["Location1"].ToString() : "";
//            }
//            set
//            {
//                if (HttpContext.Current == null)
//                {
//                    //do nothing...
//                }
//                else
//                {
//                    HttpContext.Current.Application["Location1"] = value;
//                }
//            }
//        }

//        public static void SaveToApplicationMemory(string locationName, string infoToSave)
//        {
//            if (HttpContext.Current != null)
//                HttpContext.Current.Application[locationName] = infoToSave;
//        }

//        public static string RetrieveFromApplicationMemory(string locationName)
//        {
//            if (HttpContext.Current == null)
//                return "";
    
//            return HttpContext.Current.Application[locationName] != null ? HttpContext.Current.Application[locationName].ToString() : "";
//        }
//    }
//}