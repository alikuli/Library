using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {

        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {
            State state = parm.Entity as State;

            bool isBlankState = state.Name.IsNullOrWhiteSpace();

            if (isBlankState)
            {
                List<State> blankStates = FindAll().Where(x => x.Name == "").ToList();
                State blankCountryStateFound = blankStates.FirstOrDefault(x => x.CountryId == state.CountryId);

                if (!blankCountryStateFound.IsNull())
                {
                    string err = string.Format("Blank state exists for {0}", state.Country.FullName());
                    ErrorsGlobal.AddMessage(err);
                    throw new NoDuplicateException(err);

                }
            }
            else
                base.BusinessRulesFor(parm);
            

        }

        private void noDuplicateState(State entity)
        {
            //there should be one Blank state only for a country

            //Is the current one blank?
            bool isBlankState = entity.Name.IsNullOrWhiteSpace();

            if(isBlankState)
            {
                List<State> blankStates= FindAll().Where(x => x.Name == "").ToList();
                State blankCountryStateFound = blankStates.FirstOrDefault(x => x.CountryId == entity.CountryId);

                if (!blankCountryStateFound.IsNull())
                {
                    string err = string.Format("Blank state exists for {0}", entity.Country.FullName());
                    ErrorsGlobal.AddMessage(err);
                    throw new NoDuplicateException(err);

                }
            }
            
        }
        public bool BlankStateExistsForCountryId(string id)
        {
            State s = Dal.FindAllFor().FirstOrDefault(x => x.CountryId == id && (x.Name == "" || x.Name == null));
            return !s.IsNull();
        }
    }
}
