using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Reflection;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.StateNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UowLibrary
{
    public partial class CountryBizTest : CountryBiz
    {

        public CountryBizTest(StateBiz stateBiz, IRepositry<Country> entityDal, BizParameters bizParameters)
            : base(stateBiz,entityDal, bizParameters)

        {
            loadList();
        }

        List<Country> countryList = new List<Country>();

        Country createCountry(string id, string name)
        {
            Country c1 = Factory() as Country;
            c1.Id = id;
            c1.Name = name;
            return c1;
        }

        void loadList()
        {
            Country c1 = createCountry("1", "Pakistan");
            Country c2 = createCountry("2", "India");
            Country c3 = createCountry("2", "USA");

            countryList.Add(c1);
            countryList.Add(c2);
            countryList.Add(c3);
        }

        public override IQueryable<Country> FindAll(bool deleted = false)
        {
            return countryList.AsQueryable();
        }

        public override async Task<List<Country>> FindAllAsync(bool deleted = false)
        {
            return await FindAll(deleted).ToListAsync();
        }
        

    }
}
