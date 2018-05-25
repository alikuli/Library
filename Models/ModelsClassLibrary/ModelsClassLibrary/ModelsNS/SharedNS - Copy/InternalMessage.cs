using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.CommonAndSharedNS
{
    [NotMapped]
    public class InternalMessage
    {
        public InternalMessage()
        {


        }
        //The Id for this will not be string, it will be string.
        public InternalMessage(string id, string text)
        {
            if (id.IsNullOrEmpty())
                throw new Exception("No id received. InternalMessage.InternalMessage");
            Id = id?? String.Empty;
            Text = text;
        }

        public string Id { get; set; }
        public string Text { get; set; }

        public string StringifyListOfErrorMessages( string msgFirstLIne, List<InternalMessage> lstInternalMessages)
        {
            if (lstInternalMessages == null || lstInternalMessages.Count == 0)
                return "(None)";
            StringBuilder sb = new StringBuilder();
            sb.Append (msgFirstLIne + ": ");
            foreach (var item in lstInternalMessages)
            {
                sb.Append(item + "; ");
            }

            return sb.ToString();
        }
    }
}