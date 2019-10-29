using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary.StateNS;

namespace UowLibrary
{
    public partial class CountryBiz : BusinessLayer<Country>
    {


        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {
            base.BusinessRulesFor(parm);
            Country country = parm.Entity as Country;
            //Make sure a blank state exists for the country... if not, create one.
            createCreateBlankStateForCountry(parm);
            country.Abbreviation = country.Abbreviation.ToUpper();


        }

        private void createCreateBlankStateForCountry(ControllerCreateEditParameter parm)
        {
            Country country = parm.Entity as Country;
            if (StateBiz.BlankStateExistsForCountryId(country.Id))
                return;

            State s = StateBiz.Factory() as State;
            s.Country = country;
            s.CountryId = country.Id;
            s.MetaData.IsEditLocked = true;

            ControllerCreateEditParameter parmState = new ControllerCreateEditParameter();
            parmState.Entity = s as ICommonWithId;
            StateBiz.Create(parmState);
        }

    }
}
