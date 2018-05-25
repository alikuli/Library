using EnumLibrary.EnumNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.RightsNS
{
    [NotMapped]
    public class CR_Right : Right
    {

        public CR_Right(ClassesWithRightsENUM rightsFor)
            : base(rightsFor, "")
        {
            Create = true;
            Retrieve = true;
        }


    }
}
