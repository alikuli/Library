using AliKuli.Extentions;
using DatastoreNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using System.Reflection;
using System.Web;

namespace UowLibrary
{
    public partial class CountryBiz : BusinessLayer<Country>
    {


        #region InitializationData and InitializationDataAsync



        public override void AddInitData()
        {
            string[,] countryDataArray = CountryData.CountryDataArray();
            if (!countryDataArray.IsNull() && countryDataArray.Length > 0)
            {
                for (int i = 0; i < countryDataArray.Length / 3; i++)
                {

                    string countryName = countryDataArray[i, 0];
                    string abbrev = countryDataArray[i, 1];
                    string phoneAreaCode = countryDataArray[i, 2];

                    Country c = Factory();
                    c.Abbreviation = (abbrev.IsNull() ? abbrev : abbrev.ToUpper());
                    c.Name = countryName;

                    try
                    {
                        //Create(c);
                        Create_ForInitializeOnly(c);
                    }
                    catch (NoDuplicateException e)
                    {

                        ErrorsGlobal.AddMessage(string.Format("Duplicate entry: '{0}'", c.ToString()), MethodBase.GetCurrentMethod(), e);
                    }
                }
            }

        }

        #endregion

    }
}
