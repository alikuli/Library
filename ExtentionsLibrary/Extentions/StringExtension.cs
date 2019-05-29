using System;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AliKuli.Extentions
{
    public static class StringExtension
    {






        private const string splittingSequence = "&#@";

        /// <summary>
        /// This makes the string title case, i.e. every word begins with a capital letter.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string ToNumCommaFormat(this string str)
        {
            if (str.IsNullOrWhiteSpace())
                return string.Format("{0:n2}", 0);


            decimal num;
            bool success = decimal.TryParse(str, out num);

            if (!success)
                return "Error. ToNumCommaFormat";

            string formated = string.Format("{0:n2}", num);
            return formated;
        }

        public static string ToRuppeeFormat(this string str)
        {
            if (str.IsNullOrWhiteSpace())
                return str;


            decimal num;
            bool success = decimal.TryParse(str, out num);

            if (!success)
                return str;

            string formated = string.Format("Rs{0:n2}", num);

            return formated;
        }
        public static bool IsNullOrWhiteSpaceThrowException(this string str, string errMsg = "")
        {
            if (str.IsNullOrWhiteSpace())
            {
                if (errMsg.IsNullOrWhiteSpace())
                {

                    throw new Exception(string.Format("String is null or only white space", str));
                }
                else
                {
                    string[] strArray = { " " };
                    string[] words = errMsg.Split(strArray, StringSplitOptions.RemoveEmptyEntries);
                    if (words.Count() > 1)
                    {
                        throw new Exception(errMsg);

                    }
                    else
                    {
                        throw new Exception(string.Format("String: '{0}' is null or only white space", errMsg));

                    }
                }
            }
            return string.IsNullOrWhiteSpace(errMsg);
        }

        public static bool IsNullOrWhiteSpaceThrowArgumentException(this string str, string errMsg = "")
        {
            if (str.IsNullOrWhiteSpace())
            {
                if (errMsg.IsNullOrWhiteSpace())
                    throw new ArgumentException(string.Format("String: '{0}' is null or only white space", str));
                else
                {
                    string[] strArray = { " " };
                    string[] words = errMsg.Split(strArray, StringSplitOptions.RemoveEmptyEntries);
                    if (words.Count() > 1)
                    {
                        throw new ArgumentException(errMsg);

                    }
                    else
                    {
                        throw new ArgumentException(string.Format("String: '{0}' is null or only white space", errMsg));

                    }
                }

            }
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Accepts a string and convert as DateTime. It has an optional parameter throwExceptionIfFailed. 
        /// if it is true then caller of this method should expect exception and handle it. 
        /// Default is false which means that it returns DateTime.MinValue if the string cannot be converted as DateTime
        /// </summary>
        /// <param name="input"></param>
        /// <param name="throwExceptionIfFailed"></param>
        /// <returns></returns>
        public static DateTime ToDate(this string input, bool throwExceptionIfFailed = false)
        {
            DateTime result;
            var valid = DateTime.TryParse(input, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException(string.Format("'{0}' cannot be converted as DateTime", input));
            return result;
        }


        public static bool ToBool(this string input)
        {
            bool result;
            var valid = bool.TryParse(input, out result);
            if (valid)
                return result;
            else
                throw new Exception(string.Format("Invalid boolean. It must be 'true' or 'false'. The current value is: {0}", input));

        }

        public static int ToInt(this string input, bool throwExceptionIfFailed = false)
        {
            int result;
            var valid = int.TryParse(input, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException(string.Format("'{0}' cannot be converted as int", input));
            return result;
        }
        public static long ToLong(this string input, bool throwExceptionIfFailed = false)
        {
            long result;
            var valid = long.TryParse(input, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException(string.Format("'{0}' cannot be converted as long", input));
            return result;
        }

        public static double ToDouble(this string input, bool throwExceptionIfFailed = false)
        {
            double result;
            var valid = double.TryParse(input, NumberStyles.AllowDecimalPoint,
              new NumberFormatInfo { NumberDecimalSeparator = "." }, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException(string.Format("'{0}' cannot be converted as double", input));
            return result;
        }

        /// <summary>
        /// This removes anything that is not a digit or an alphabet (also removes spaces)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string ToAlphaNumericStringWithoutSpace(this string input)
        {
            string regexPattern = "[^0-9a-zA-Z]";
            string output = Regex.Replace(input, regexPattern, "");
            return output;

        }


        /// <summary>
        /// This removes anything that is not a digit(also removes spaces)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string ToNumericString(this string input)
        {
            string regexPattern = "[^0-9]";
            string output = Regex.Replace(input, regexPattern, "");
            return output;

        }
        public static string Reverse(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }



        /// <summary>
        /// Matching all capital letters in the input and seperate them with spaces to form a sentence.
        /// If the input is an abbreviation text, no space will be added and returns the same input.

        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSentence(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            //return as is if the input is just an abbreviation
            if (Regex.Match(input, "/[A-Z]+$").Success)
                return input;

            //add a space before each capital letter, but not the first one.
            var result = Regex.Replace(input, "(\\B[A-Z])", " $1");
            var newResult = Regex.Replace(result, "[_]", " ");

            return newResult;
        }

        /// <summary>
        /// Matching all capital letters in the input and seperate them with spaces to form a sentence.
        /// If the input is an abbreviation text, no space will be added and returns the same input.
        /// Then capitalizes every word.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToTitleSentance(this string input)
        {
            return input.ToSentence().ToTitleCase();
        }
        public static string GetLast(this string input, int howMany)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var value = input.Trim();
            return howMany >= value.Length ? value : value.Substring(value.Length - howMany);
        }

        public static string GetFirst(this string input, int howMany)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var value = input.Trim();
            return howMany >= value.Length ? value : input.Substring(0, howMany);
        }

        public static bool IsEmail(this string input)
        {
            var match = Regex.Match(input,
              @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success;
        }


        public static bool IsPhone(this string input)
        {
            var match = Regex.Match(input,
              @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static bool IsInt(this string input)
        {
            int result;
            var success = int.TryParse(input, out result);
            return success;
        }

        public static bool IsDouble(this string input)
        {
            double result;
            var success = double.TryParse(input, out result);
            return success;
        }
        public static int ExtractNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            var match = Regex.Match(input, "[0-9]+", RegexOptions.IgnoreCase);
            return match.Success ? match.Value.ToInt() : 0;
        }

        public static string ExtractEmail(this string input)
        {
            if (input == null || string.IsNullOrWhiteSpace(input)) return string.Empty;

            var match = Regex.Match(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success ? match.Value : string.Empty;
        }

        public static string ExtractQueryStringParamValue(this string queryString, string paramName)
        {
            if (string.IsNullOrWhiteSpace(queryString) || string.IsNullOrWhiteSpace(paramName)) return string.Empty;

            var query = queryString.Replace("?", "");

            if (!query.Contains("=")) return string.Empty;

            var queryValues = query.Split('&').Select(piQ => piQ.Split('=')).ToDictionary(
              piKey => piKey[0].ToLower().Trim(), piValue => piValue[1]);
            string result;
            var found = queryValues.TryGetValue(paramName.ToLower().Trim(), out result);
            return found ? result : string.Empty;
        }

        /// <summary>
        /// This creates a string with a seperator for passing on. The defualt seperator is
        /// "#@$*"
        /// </summary>
        /// <param name="stringIn"></param>
        /// <param name="addString"></param>
        /// <param name="splitStringOn"></param>
        /// <returns></returns>
        public static string ConcatStrWithSeperator(this string stringIn, string addString, string splitStringOn = splittingSequence)
        {
            string internalString = string.IsNullOrWhiteSpace(stringIn) ? "" : stringIn;

            if (!string.IsNullOrWhiteSpace(addString))
            {
                if (string.IsNullOrWhiteSpace(internalString))
                {
                    internalString = addString;

                }
                else
                {
                    string errorStrWithNumber = string.Format("{0}{1}", splitStringOn, addString);
                    internalString += errorStrWithNumber;
                }
            }
            return internalString;

        }

        /// <summary>
        /// This splits a string into an array of strings. The default seperator is "#@$*"
        /// </summary>
        /// <param name="stringIn"></param>
        /// <param name="splitOn"></param>
        /// <returns></returns>
        public static string[] Concat_NowSplitStringWithSeperator(this string stringIn, string splitOn = splittingSequence)
        {
            if (!string.IsNullOrWhiteSpace(stringIn))
            {
                //split the string on the "#"
                string[] strArray = stringIn.Split(new string[] { splitOn }, System.StringSplitOptions.None);

                return strArray.Count() > 0 ? strArray : null; ;
            }

            return null;
        }

        public static string ConvertStrNumTo16DigitFormat(this string str16DigitNum)
        {
            //first make sure there are 16 digits
            if (str16DigitNum.Length == 16)
            {
                long theNumber = 0;
                bool success = long.TryParse(str16DigitNum, out theNumber);

                if (success)
                    return string.Format("{0:0000 0000 0000 0000}", theNumber);


            }

            return str16DigitNum;
        }

        public static string ConvertPakistanIdFormat(this string str13DigitNum)
        {
            //first make sure there are 16 digits
            if (str13DigitNum.Length == 13)
            {
                long theNumber = 0;
                bool success = long.TryParse(str13DigitNum, out theNumber);

                if (success)
                    return string.Format("{0:00000-0000000-0}", theNumber);


            }

            return str13DigitNum;
        }

        public static string ToPakistanPhoneFormat(this string pkCompletePhoneNo)
        {
            //first make sure there are 16 digits
            if (pkCompletePhoneNo.Length == 12)
            {
                long theNumber = 0;
                bool success = long.TryParse(pkCompletePhoneNo, out theNumber);

                if (success)
                    return string.Format("{0:(00) 000-000-0000}", theNumber);


            }

            return pkCompletePhoneNo;
        }

        public static string ToPakistanCnicFormat(this string pkCnicNum)
        {
            //first make sure there are 16 digits
            if (pkCnicNum.Length == 13)
            {
                long theNumber = 0;
                bool success = long.TryParse(pkCnicNum, out theNumber);

                if (success)
                    return string.Format("{0:00000-000000-0}", theNumber);


            }

            return pkCnicNum;
        }
        /// <summary>
        /// This function removes all the spaces from a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveAllSpaces(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            string s = Regex.Replace(input, @"\s+", string.Empty);
            return s;
        }

        public static string RemoveAllDashes(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            //https://stackoverflow.com/questions/33643637/regex-to-remove-dash-between-alphanumeric
            string s = Regex.Replace(input, @"(?<=\w)-(?=\w)", string.Empty);
            return s;
        }

        /// <summary>
        /// This replaces all the spaces with underscores
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertSpacesToUnderScores(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            string s = input.Replace(' ', '_');
            return s;

        }



        #region Validators

        /// <summary>
        /// This validates email addresses
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string emailaddress)
        {
            if (string.IsNullOrWhiteSpace(emailaddress))
                return false;

            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public static bool IsValidUrl(this string uriName)
        {
            Uri uriResult;
            return Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
                     && (uriResult.Scheme == Uri.UriSchemeHttp
                     || uriResult.Scheme == Uri.UriSchemeHttps);

        }
        public static bool IsValidFilePath(this string uriName)
        {
            throw new NotImplementedException("IsValidFilePath");

        }
        public static bool IsValidBoolean(this string booleanValue)
        {
            string ucValue = booleanValue.ToUpper();

            switch (ucValue)
            {
                case "TRUE": return true;
                case "FALSE": return true;
                case "YES": return true;
                case "NO": return true;
                default: return false;
            }

        }
        //public static bool IsValidString(this string integerValue)
        //{

        //    throw new NotImplementedException("IsValidString");
        //}
        public static bool IsValidInteger(this string integerValue)
        {
            if (integerValue.IsNullOrEmpty())
                return false;

            int intOut;
            return int.TryParse(integerValue, out intOut);
        }

        public static bool IsValidLong(this string longValue)
        {
            if (longValue.IsNullOrWhiteSpace())
                return false;

            long l;
            return long.TryParse(longValue, out l);
        }

        public static bool IsValidDecimal(this string decimalValue)
        {
            if (decimalValue.IsNullOrWhiteSpace())
                return false;

            decimal d;
            return decimal.TryParse(decimalValue, out d);
        }
        public static bool IsValidEmailingMethod(this string emailMethod)
        {
            try
            {
                if (string.IsNullOrEmpty(emailMethod))
                    return false;
                //    throw new Exception("The Email address is empty!");

                MailAddress m = new MailAddress(emailMethod);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

    }
}