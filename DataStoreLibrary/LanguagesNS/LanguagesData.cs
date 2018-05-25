using System.Collections.Generic;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of languages that the worker can speak or understand.
    /// </summary>
    public class LanguagesData
    {
        public static string[] DataArray()
        {
            string[] languagesArray = 
            {
                "Urdu",
                "English",
                "Siraki",
                "Pashto",
                "Balochi",
                "Hindi"

            };
            return languagesArray;

        }
    }
}
