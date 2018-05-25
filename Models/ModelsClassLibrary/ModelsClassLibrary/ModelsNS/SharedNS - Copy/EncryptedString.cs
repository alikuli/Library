using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [ComplexType]
    public class EncryptedString
    {

        [MaxLength(1000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public virtual string Value { get; set; }

    }
}
