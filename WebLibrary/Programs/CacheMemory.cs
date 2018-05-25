using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace AliKuli.UtilitiesNS
{

    /// <summary>
    /// Use this utility to manipulate application memory for your own use.
    /// </summary>
    public class CacheMemory : MemoryAbstract
    {
        public CacheMemory(HttpContextBase httpCtx)
            : base(httpCtx)
        {

        }

        public override bool Add(string location, object obj)
        {
            if (HttpCtx == null)
            {
                throw new Exception("HttpCtx is null. Add");
            }

            ////System.Web.Caching.CacheDependency dependancy = new System.Web.Caching.CacheDependency("Test");
            //DateTime absoluteExpiration = DateTime.Now.AddMinutes(60);
            //TimeSpan slidingExpiration = TimeSpan.FromMinutes(60);
            //HttpCtx.Cache.Add(location, obj, null, absoluteExpiration, slidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);

            HttpCtx.Cache[location] = obj;
            return true;
        }

        public bool Add(string location, object obj, TimeSpan slidingExpiration)
        {
            if (HttpCtx == null)
            {
                throw new Exception("HttpCtx is null. Add");
            }

            HttpCtx.Cache.Add(
                location,
                obj,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                slidingExpiration,
                CacheItemPriority.Normal,
                null);

            return true;
        }

        public override object Retrieve(string location)
        {
            if (HttpCtx == null)
            {
                throw new Exception("HttpCtx is null. Retreive");
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                throw new Exception("No location received. Retreive");

            }
            return (object)HttpCtx.Cache[location];



        }

        //public override void ClearFor(string location)
        //{
        //    HttpCtx.Cache.Remove(location);
        //}

        public override bool ClearFor(string locationName)
        {
            HttpCtx.Cache.Remove(locationName);
            return true;
        }
    }
}
