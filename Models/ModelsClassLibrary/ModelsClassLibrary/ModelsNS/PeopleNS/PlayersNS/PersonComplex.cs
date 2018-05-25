using System.ComponentModel.DataAnnotations.Schema;
using ModelsClassLibrary.ModelsNS.SharedNS.Common;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS
{



    /// <summary>
    /// This class is only used to do calculations etc for IPerson. 
    /// </summary>
    [ComplexType]
    public class PersonComplex : Person
    {
    }
}