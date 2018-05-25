using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using System.Linq;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {

        public override void BusinessRulesFor(State entity)
        {
            base.BusinessRulesFor(entity);


        }
        public bool BlankStateExistsForCountryId(string id)
        {
            State s = Dal.FindAll().FirstOrDefault(x => x.CountryId == id && (x.Name == "" || x.Name == null));
            return !s.IsNull();
        }
    }
}
