using System;
using System.Collections.Generic;
using InterfacesLibrary.SharedNS;
using UserModels;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;

namespace ModelsClassLibrary.RightsNS

{
    
    public partial class Right : CommonWithId, IRight
    {
        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            Right r = (Right)icommonWithId;

            Create = r.Create;
            Retrieve = r.Retrieve;
            Update = r.Update;
            Delete = r.Delete;
            DeleteActually = r.DeleteActually;
            UserId = r.UserId;

        }
    }
}