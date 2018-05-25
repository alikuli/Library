using System;
using AliKuli.Extentions;
namespace AliKuli.UtilitiesNS
{

    /// <summary>
    /// Use this utility to manipulate application memory for your own use.
    /// </summary>
    public class SessionMemory : MemoryAbstract
    {


        /// <summary>
        /// Send in the httpContext
        /// </summary>
        /// <param name="httpCtx"></param>
        public SessionMemory(System.Web.HttpContextBase httpCtx)
            : base(httpCtx)
        {

        }

        public override bool Add(string location, object obj)
        {
            if (HttpCtx == null)
            {
                throw new Exception("HttpCtx is null. Add");

            }


            HttpCtx.Session[location] = obj;
            return true;
        }


        public override object Retrieve(string location)
        {
            if (HttpCtx.IsNull())
            {
                throw new Exception("HttpCtx is null. Add");
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                throw new Exception("No location received. Retrieve");

            }
            return (object)HttpCtx.Session[location];

        }









        public override bool ClearFor(string locationName)
        {
            HttpCtx.Session.Remove(locationName);
            return true;
        }
    }
}