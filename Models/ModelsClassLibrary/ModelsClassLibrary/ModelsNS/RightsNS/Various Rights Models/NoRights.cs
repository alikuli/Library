using EnumLibrary.EnumNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.RightsNS
{
    [NotMapped]
    public class NoRights : Right
    {

        public NoRights(ClassesWithRightsENUM rightsFor)
            : base (rightsFor, "")
        {
            Create = false;
            Retrieve = false;
            Update = false;
            Delete = false;
            DeleteActually = false;

        }


    }
}
