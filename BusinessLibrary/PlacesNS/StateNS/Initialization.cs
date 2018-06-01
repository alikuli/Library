using AliKuli.Extentions;
using DatastoreNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using System;
using System.Linq;
using System.Reflection;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {

        public override void AddInitData()
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
                    CreateSave_ForInitializeOnly(s);
                    //Create(s);
                }
                catch (NoDuplicateException e)
                {

                    ErrorsGlobal.AddMessage(string.Format("Duplicate entry: '{0}'", s.ToString()), MethodBase.GetCurrentMethod(), e);
                }


            }
        }
    }
}
