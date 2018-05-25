using System.ComponentModel.DataAnnotations;

namespace EnumLibrary.EnumNS
{

    public enum SonOfWifeOfDotOfENUM
    {
        Unknown,
        //string values from http://stackoverflow.com/q/2787506/3777098
        [Display(Name = "Son Of")]

        SonOf,

        [Display(Name = "Wife Of")]
        WifeOf,

        [Display(Name = "Daughter Of")]
        DaughterOf,

    }
}
