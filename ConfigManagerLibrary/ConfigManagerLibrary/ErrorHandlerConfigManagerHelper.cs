using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigManagerLibrary
{

    public class ErrorHandlerConfigManagerHelper
    {
        public ErrorHandlerConfigManagerHelper(bool showDetailedError = true)
        {
            ShowDetailedError = showDetailedError;
        }
        public bool ShowDetailedError { get; set; }
        public ICollection<string> Errors { get; private set; }
        public string Heading { get { return "Config Manager Helper"; } }


        public void AddError(string message, string methodName ="")
        {
            StringBuilder sb = new StringBuilder();

            if (ShowDetailedError)
            {
                sb.Append(Heading + ". ");
                string dateIs = DateTime.UtcNow.ToLongDateString() + " - " + DateTime.UtcNow.ToLongTimeString();
                sb.Append(dateIs + ". ");
                sb.Append(methodName + ". ");
            }
            sb.Append(message);
            Errors.Add(sb.ToString());
        }

    }
}
