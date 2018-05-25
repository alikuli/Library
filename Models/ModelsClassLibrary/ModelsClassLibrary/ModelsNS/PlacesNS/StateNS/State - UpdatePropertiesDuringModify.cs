using System;
//using InterfacesLibrary.DiscountNS;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{

    /// <summary>
    /// This stores the state. This also stores abbreviation for a state. Abbreviation is optional.
    /// Sometimes countries do not have states. For those countries, whenever we create a country, we
    /// create a default state. This default state has the same name as the country. It also has the 
    /// same abbreivation as the country. When the index is being created, then this will not be allowed 
    /// to be edited. tHE AutoCreated field is set to be true.
    /// </summary>

    
    public partial class State : StateAbstract
    {
        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            State s = (State)icommonWithId;
            Abbreviation = s.Abbreviation;
        }
    }
}