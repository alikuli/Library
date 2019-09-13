using AliKuli.Extentions;
using System.Collections.Generic;

namespace AliKuli.ToolsNS
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
        // @"\Content\SetupData\stop-word-list.csv"
        public static string[] GetStopWords(string fromPath)
        {
            string[] stopWords = FileTools.ParseCsvCommaDelimited(FileTools.GetAbsolutePath(fromPath));

            //now make them all lower case...
            stopWords.IsNullOrEmptyThrowException("Stop words not found.");

            for (int i = 0; i < stopWords.Length; i++)
            {
                stopWords[i] = stopWords[i].ToLower().Trim();
            }
            return stopWords;


        }

    }
}
