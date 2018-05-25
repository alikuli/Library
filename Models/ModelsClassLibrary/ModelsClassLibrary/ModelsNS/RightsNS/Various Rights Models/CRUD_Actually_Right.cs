using EnumLibrary.EnumNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.RightsNS
{
    [NotMapped]
    public class CRUD_Actually_Right : Right
    {

        public CRUD_Actually_Right(ClassesWithRightsENUM rightsFor)
            : base(rightsFor, "")
        {

            Create = true;
            Retrieve = true;
            Update = true;
            Delete = true;
            DeleteActually = true;
        }


    }
}
