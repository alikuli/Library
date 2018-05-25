using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using System;
using System.Reflection;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {


        public override void Fix(State entity)
        {
            base.Fix(entity);

            if (!entity.Abbreviation.IsNullOrWhiteSpace())
                entity.Abbreviation = entity.Abbreviation.ToUpper();

            fixTheCountry(entity);
        }






        private void fixTheCountry(State entity)
        {
            if (entity.CountryId.IsNullOrEmpty())
            {
                if (entity.Country.IsNull())
                {
                    ErrorsGlobal.Add("No country has been added", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
            }

            if (entity.CountryId.IsNullOrEmpty())
                entity.CountryId = entity.Country.Id;

            if (entity.Country.IsNull())
                entity.Country = CountryDal.FindFor(entity.CountryId);
        }



    }
}
