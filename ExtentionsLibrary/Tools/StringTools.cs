using System.Collections.Generic;

namespace AliKuli.Tools
{
    public class StringTools
    {

        public static string ConvertListToCommaString(List<string> lst)
        {
            string s = "";
            if (lst != null && lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    s += item + ", ";
                }

                s = s.Substring(0, s.Length - 2);
            }

            return s;
        }

    }
}
