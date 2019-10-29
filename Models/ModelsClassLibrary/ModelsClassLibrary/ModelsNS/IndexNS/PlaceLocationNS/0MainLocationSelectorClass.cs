using ModelsClassLibrary.ModelsNS.AddressNS;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
namespace ModelsClassLibrary.ModelsNS.IndexNS.PlaceLocationNS
{
    public class MainLocationSelectorClass
    {
        public MainLocationSelectorClass()
        {

        }

        public List<Country> Countries { get; set; }


        public void AddCountries(List<AddressMain> addressList)
        {

            if (addressList.IsNullOrEmpty())
                Countries = new List<Country>();
            List<string> countryNames = new List<string>();

            foreach (AddressMain address in addressList)
            {
                if (address.CountryName.IsNullOrWhiteSpace())
                    continue;

                string countryName = address.CountryName.ToTitleCase();
                countryNames.Add(countryName);
            }

            List<string> countryNamesDistinct = new HashSet<string>(countryNames)
                .ToList()
                .OrderBy(x => x)
                .ToList();
        
            if(countryNamesDistinct.IsNullOrEmpty())
                Countries = new List<Country>();
            
            List<Country> countriesList = new List<Country>();
            foreach (string countryName in countryNamesDistinct)
            {
                Country countryLocation = new Country();
                countryLocation.Name = countryName;
                countryLocation.States = getStatesForCountry(addressList,countryName);
                countriesList.Add(countryLocation);
            }

            Countries = countriesList;
        }

        private List<State> getStatesForCountry(List<AddressMain> addressList, string countryName)
        {
            if(addressList.IsNullOrEmpty())
                return new List<State>();

            countryName.IsNullOrWhiteSpaceThrowException();

            List<AddressMain> addressWithStateTemp = addressList
                .Where(x => x.CountryName.ToLower() == countryName.ToLower())
                .ToList();

            if (addressWithStateTemp.IsNullOrEmpty())
                return new List<State>();

            List<string> stateNamesForCountry = new List<string>();
            foreach (AddressMain addy in addressWithStateTemp)
            {
                if (addy.StateName.IsNullOrEmpty())
                    continue;
                stateNamesForCountry.Add(addy.StateName.ToTitleCase());
            }

            if(stateNamesForCountry.IsNullOrEmpty())
                return new List<State>();

            List<string> stateNamesForCountryDistinct = new HashSet<string>(stateNamesForCountry)
                .ToList()
                .OrderBy(x => x)
                .ToList();

            if(stateNamesForCountryDistinct.IsNullOrEmpty())
                return new List<State>();

            List<State> statesDistinctForCountry = new List<State>();
            foreach (string stateName in stateNamesForCountryDistinct)
            {
                if (stateName.IsNullOrWhiteSpace())
                    continue;
                State stateLoc = new State();
                stateLoc.Name = stateName;
                stateLoc.Cities = getCitiesForCityNameFor(addressList, stateName);
                statesDistinctForCountry.Add(stateLoc);
            }

            return statesDistinctForCountry;
        }

        private List<City> getCitiesForCityNameFor(List<AddressMain> addressList, string stateName)
        {
            if (addressList.IsNullOrEmpty())
                return new List<City>();

            stateName.IsNullOrWhiteSpaceThrowException();

            List<AddressMain> addressWithCityTemp = addressList
                .Where(x => x.StateName.ToLower() == stateName.ToLower())
                .ToList();

            if (addressWithCityTemp.IsNullOrEmpty())
                return new List<City>();


            List<string> cityNamesForState = new List<string>();
            foreach (AddressMain addy in addressWithCityTemp)
            {
                if (addy.CityName.IsNullOrEmpty())
                    continue;
                cityNamesForState.Add(addy.CityName.ToTitleCase());
            }

            if (cityNamesForState.IsNullOrEmpty())
                return new List<City>();

            List<string> cityNamesForCountryDistinct = new HashSet<string>(cityNamesForState)
                .ToList()
                .OrderBy(x => x)
                .ToList();

            if (cityNamesForCountryDistinct.IsNullOrEmpty())
                return new List<City>();

            List<City> citysDistinctForCountry = new List<City>();
            foreach (string cityName in cityNamesForCountryDistinct)
            {
                if (cityName.IsNullOrWhiteSpace())
                    continue;
                City cityLoc = new City();
                cityLoc.Name = cityName;
                cityLoc.Towns = getTownsForCityNameFor(addressList,cityName);
                citysDistinctForCountry.Add(cityLoc);
            }

            return citysDistinctForCountry;
        }


        private List<Town> getTownsForCityNameFor(List<AddressMain> addressList, string cityName)
        {
            if (addressList.IsNullOrEmpty())
                return new List<Town>();

            cityName.IsNullOrWhiteSpaceThrowException();

            List<AddressMain> addressWithTownTemp = addressList
                .Where(x => x.CityName.ToLower() == cityName.ToLower())
                .ToList();

            if (addressWithTownTemp.IsNullOrEmpty())
                return new List<Town>();

            List<string> townNamesForState = new List<string>();
            foreach (AddressMain addy in addressWithTownTemp)
            {
                if (addy.TownName.IsNullOrEmpty())
                    continue;
                townNamesForState.Add(addy.TownName.ToTitleCase());
            }

            if (townNamesForState.IsNullOrEmpty())
                return new List<Town>();

            List<string> townNamesForCountryDistinct = new HashSet<string>(townNamesForState)
                .ToList()
                .OrderBy(x => x)
                .ToList();

            if (townNamesForCountryDistinct.IsNullOrEmpty())
                return new List<Town>();

            List<Town> townsDistinctForCountry = new List<Town>();
            foreach (string townName in townNamesForCountryDistinct)
            {
                if (townName.IsNullOrWhiteSpace())
                    continue;
                Town townLoc = new Town();
                townLoc.Name = townName;
                townsDistinctForCountry.Add(townLoc);
            }

            return townsDistinctForCountry;
        }

    }


}
