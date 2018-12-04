using System;
using System.Collections.Generic;
using InterfacesLibrary.SharedNS;
using UserModels;
using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.PlacesNS
{
    
    public partial class Country : CountryAbstract, ICommonWithId
    {
        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            
            Country c = (Country)icommonWithId;
            c.IsNullThrowException("Unable to unbox country");

            Abbreviation = c.Abbreviation;

        }
    }
}