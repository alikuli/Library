using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary.StateNS;

namespace UowLibrary
{
    public partial class CountryBiz : BusinessLayer<Country>
    {


        public override void BusinessRulesFor(Country entity)
        {
            base.BusinessRulesFor(entity);

            //Make sure a blank state exists for the country... if not, create one.
            createCreateBlankStateForCountry(entity);
            entity.Abbreviation = entity.Abbreviation.ToUpper();


        }

        private void createCreateBlankStateForCountry(Country entity)
        {
            if (StateBiz.BlankStateExistsForCountryId(entity.Id))
                return;

            State s = StateBiz.Factory();
            s.Country = entity;
            s.CountryId = entity.Id;
            s.MetaData.IsEditLocked = true;
            StateBiz.Create(s);
        }

    }
}
