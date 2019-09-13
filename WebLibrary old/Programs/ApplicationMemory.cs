using System;
using System.Web;
using AliKuli.Extentions;
namespace AliKuli.UtilitiesNS
{

    /// <summary>
    /// Use this utility to manipulate application memory for your own use.
    /// </summary>
    public class ApplicationMemory : MemoryAbstract
    {


        /// <summary>
        /// Send in the httpContext
        /// </summary>
        /// <param name="httpCtx"></param>
        public ApplicationMemory(HttpContextBase httpCtx)
            : base(httpCtx)
        {

        }

        public override bool Add(string location, object obj)
        {
            if (HttpCtx == null)
            {
                throw new Exception("HttpCtx is null. Add");

            }


            HttpCtx.Application[location] = obj;
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
            return (object)HttpCtx.Application[location];

        }

        const string USERNAME = "username";
        public string UserName
        {
            get
            {
                object o = Retrieve(USERNAME);
                if (o.IsNull())
                    return "";
                return o.ToString();
            }

            set
            {
                Add(USERNAME, value);
            }
        }






        public override bool ClearFor(string locationName)
        {
            HttpCtx.Application.Remove(locationName);
            return true;
        }
    }
}