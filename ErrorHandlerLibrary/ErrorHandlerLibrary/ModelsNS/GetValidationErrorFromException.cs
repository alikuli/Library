using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace ErrorHandlerLibrary.ExceptionsNS
{
    public static class GetValidationErrorFromException
    {
        public static string Get_DbEntityValidationException(DbEntityValidationException e)
        {
            List<String> lstErrors = new List<string>();
            StringBuilder sb = new StringBuilder();

            if (e.EntityValidationErrors != null && e.EntityValidationErrors.Count() > 0)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    string msg = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);

                    lstErrors.Add(msg);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        lstErrors.Add(msg);
                    }
                }

                if (lstErrors != null && lstErrors.Count() > 0)
                {
                    foreach (var item in lstErrors)
                    {
                        sb.Append(item + "; ");
                    }

                    return ("Db Entity Validation Exception. Data not saved. Error: " + sb.ToString());

                }


                //Now add any inner Exception errors
                sb.Append("Inner Exception is: " + ErrorHelpers.GetInnerException(e));

            }

            return sb.ToString();
        }

    }
}