using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.ClicksNS
{
    public class Click : CommonWithId
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);

            Click c = ic as Click;

            if (c == null)
                throw new Exception("Unable to box Click. Click.UpdatePropertiesDuringModify ");

            UserId = c.UserId;
        }
    }


}
