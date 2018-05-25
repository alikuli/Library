using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UserModels;

namespace ModelsClassLibrary.RightsNS
{
    /// <summary>
    /// This will be used to give users rights to CRUD. This particular user will be allowed to do whatever is allowed here.
    /// The CreateChildren will allow the user to give rights to their account.
    /// This contains everything you need to give a user his rights
    /// </summary>
    public partial class Right : CommonWithId, IRight
    {

        /// <summary>
        /// We dont want the Name to be sentance cased as it is the Id + Name of class. Leave it as is.
        /// </summary>
        public override bool IsAllowNameToBeSentanceCased
        {
            get
            {
                return false;
            }
        }
    }
}
