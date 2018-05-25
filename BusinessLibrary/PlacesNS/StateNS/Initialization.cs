using System;
using System.Linq;
using System.Reflection;
using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using DatastoreNS;
using System.Web;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {

        public override void AddInitData(HttpContext ctx)
        {
            var data = StateData.Data();
            if (data.IsNullOrEmpty())
            {
                return;
            }

            foreach (var state in data)
            {
                State s = Dal.Factory();
                s.Name = state.Name.ToTitleCase();

                var country = CountryDal
                    .FindAll()
                    .ToList()
                    .FirstOrDefault(x => x.Abbreviation.ToLower() == state.CountryCode.ToLower());

                if (country.IsNull())
                {
                    throw new Exception(string.Format("Unable to locate country with abbreviation: '{0}'", state.CountryCode));
                }

                s.Country = country;
                s.CountryId = country.Id;

                try
                {
                    Create(s);
                }
                catch (NoDuplicateException e)
                {

                    ErrorsGlobal.AddMessage(string.Format("Duplicate entry: '{0}'", s.ToString()), MethodBase.GetCurrentMethod(), e);
                }


            }
        }
    }
}
