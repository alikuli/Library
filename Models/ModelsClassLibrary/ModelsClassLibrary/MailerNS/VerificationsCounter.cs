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
    public class VerificationsCounter
    {
        public long PakistanPostalVerificationsAvailable { get; set; }
        public long PakistanCourierVerificationsAvailable { get; set; }
        public long ForeignPostalVerificationsAvailable { get; set; }
        public long ForeignCourierVerificationsAvailable { get; set; }



        public long TotalPostal { get { return PakistanPostalVerificationsAvailable + ForeignPostalVerificationsAvailable; } }
        public long TotalCourier { get { return PakistanCourierVerificationsAvailable + ForeignCourierVerificationsAvailable; } }
        public long Total { get { return TotalPostal + TotalCourier; } }
    }
}
