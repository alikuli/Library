using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS
{
    /// <summary>
    /// This is used to search for National Id Cards after it is decrypted.
    /// </summary>
    [NotMapped]
    
    public class UserNationIdCardsVM
    {
        public string Id { get; set; }
        public string NationalIdCardDecrypted { get; set; }
    }
}