using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS.Complex
{
    [ComplexType]
    public class StringDateAndByComplex : DateAndByComplex
    {
        public string Value{ get; set; }


    }
}
