using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UserNameSpace
{
    [ComplexType]
    public class UserActive
    {
        public UserActive()
        {
            Value = false;
            MetaData = new DateAndByComplex();

        }
        public bool Value { get; set; }
        public DateAndByComplex MetaData { get; set; }
    }
}
