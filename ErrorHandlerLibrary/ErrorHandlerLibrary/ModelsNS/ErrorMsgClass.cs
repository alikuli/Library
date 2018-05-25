using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using AliKuli.Extentions;
namespace ErrorHandlerLibrary.ExceptionsNS
{
    public static class ErrorHelpers
    {

        /// <summary>
        /// This sets up the recursive function
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetInnerException(Exception e)
        {
            string message = "";
            if (e.IsNull())
            {
            }
            else
            {
                if (!e.InnerException.IsNull())
                {
                    message = GetInnerException(e.InnerException);
                }
                else
                {
                    if (!e.Message.IsNullOrWhiteSpace())
                        message = e.Message;
                }
            }

            return message;
        }


        public static string Get_DbEntityValidationException(DbEntityValidationException e)
        {
            List<String> lstErrors = new List<string>();
            StringBuilder sb = new StringBuilder();

            if (!e.IsNull())
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

                if (!lstErrors.IsNullOrEmpty())
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