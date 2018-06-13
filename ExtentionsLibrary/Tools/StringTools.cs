using AliKuli.Extentions;
using AliKuli.ToolsNS;
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
        public static string[] GetStopWords()
        {
            string[] stopWords = FileTools.ParseCsvCommaDelimited(FileTools.GetPath(@"\Content\SetupData\stop-word-list.csv"));

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
