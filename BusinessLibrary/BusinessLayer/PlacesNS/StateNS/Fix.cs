using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            State state = parm.Entity as State;
            if (!state.Abbreviation.IsNullOrWhiteSpace())
                state.Abbreviation = state.Abbreviation.ToUpper();

            //fixTheCountry(state);
        }






        private void fixTheCountry(State entity)
        {
            throw new NotImplementedException();
        //    if (entity.CountryId.IsNullOrEmpty())
        //    {
        //        if (entity.Country.IsNull())
        //        {
        //            ErrorsGlobal.Add("No country has been added", MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());
        //        }
        //    }

        //    if (entity.CountryId.IsNullOrEmpty())
        //        entity.CountryId = entity.Country.Id;

        //    if (entity.Country.IsNull())
        //        entity.Country = CountryBiz.Find(entity.CountryId);
        }



    }
}
