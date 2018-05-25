using AliKuli.Extentions;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of languages that the worker can speak or understand.
    /// </summary>
    public class OwnerCategoryData
    {
        public static string[] DataArray()
        {
            string[] ownerCatArray = { "Shop", "Service Man" };

            if (ownerCatArray.IsNullOrEmpty())
                return null;


            for (int i = 0; i < ownerCatArray.Length; i++)
            {
                ownerCatArray[i] = ownerCatArray[i].ToTitleSentance();
            }

            return ownerCatArray;

        }
    }
}
