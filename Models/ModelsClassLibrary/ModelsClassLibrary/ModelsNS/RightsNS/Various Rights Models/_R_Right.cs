using EnumLibrary.EnumNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.RightsNS
{
    [NotMapped]
    public class _R_Right : Right
    {

        public _R_Right(ClassesWithRightsENUM rightsFor)
            : base(rightsFor, "")
        {

            Retrieve = true;

        }


    }
}
