using EnumLibrary.EnumNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.RightsNS
{
    [NotMapped]
    public class CRU_Right : Right
    {

        public CRU_Right(ClassesWithRightsENUM rightsFor)
            : base(rightsFor, "")
        {
            Create = true;
            Retrieve = true;
            Update = true;


        }


    }
}
