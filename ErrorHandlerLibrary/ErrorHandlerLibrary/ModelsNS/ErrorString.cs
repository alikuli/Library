//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using AliKuli.Extentions;

//namespace ErrorHandlerLibrary.ExceptionsNS
//{
//    /// <summary>
//    /// This builds an error string and then returns it in the form of a rebuilt string. Use this when you want to save multiple errors.
//    /// </summary>
//    public static class ErrorStringBuilder
//    {

//        private static string _errorString;


//        public static string ErrorString
//        {
//            get
//            {

//                if (_errorString.IsNullOrEmpty())
//                    return "";

//                string _newString = "";
//                var  arrayErrorStringValues =_errorString.Concat_NowSplitStringWithSeperator();

//                for (int i = 0; i < arrayErrorStringValues.Length; i++)
//                {
//                    _newString +=  arrayErrorStringValues[i] + ". ";
//                }

//                return _newString.Trim(); ;
//            }
//            set
//            {
//                string valueIn = value;

//                if (!valueIn.IsNullOrEmpty())
//                {
//                    _errorString = _errorString.ConcatStrWithSeperator(valueIn);
//                }

//            }
//        }

//        public static void ClearString()
//        {
//                _errorString = "";
//        }
//    }
//}