using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class IdNameModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        [Display(Name="Verification Code")]
        public string VerificationCode { get; set; }
    }
}
