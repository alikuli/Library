using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS
{
    /// <summary>
    /// This is a counter for Mail
    /// </summary>
    public class MailCounterModel
    {
        public int Unknown { get; set; }
        public int Inproccess { get; set; }
        public int TotalOutstanding { get { return Unknown + Inproccess; } }
    }
}
