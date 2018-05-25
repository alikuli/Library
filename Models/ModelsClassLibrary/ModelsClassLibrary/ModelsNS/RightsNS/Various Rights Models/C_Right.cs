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

    public class C_Right:Right
    {

        public C_Right(ClassesWithRightsENUM rightsFor)
            : base (rightsFor, "")
        {

            Create = true;


        }


    }
}
