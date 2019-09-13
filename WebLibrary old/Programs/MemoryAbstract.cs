using System.Web;
using WebLibrary;
using AliKuli.Extentions;
using System;

namespace AliKuli.UtilitiesNS
{

    /// <summary>
    /// Use this utility to manipulate application memory for your own use.
    /// </summary>
    public abstract class MemoryAbstract 
    {
        private readonly System.Web.HttpContext _httpCtx;

        /// <summary>
        /// Send in the httpContext
        /// </summary>
        /// <param name="httpCtx"></param>
        public MemoryAbstract(HttpContextBase httpCtx)
        {
            _httpCtx = httpCtx.ApplicationInstance.Context;

        }

        public abstract bool Add(string location, object obj);


        public abstract object Retrieve(string location);



        public System.Web.HttpContext HttpCtx 
        { 
            get 
            { 
                return _httpCtx; 
            } 
        }


        public const string location1 = "Location1";
        /// <summary>
        /// Saves and retrieves an object from memory. use if for fast access
        /// </summary>
        public object Memory
        {
            get
            {
                return Retrieve(location1);
            }
            set
            {
                Add(location1, (object)value);
            }
        }

        public bool Clear()
        {
            return ClearFor(location1);
        }


        /// <summary>
        /// use this to save to a spot of your choice in Memory.
        /// </summary>
        /// <param name="locationName"></param>
        /// <param name="infoToSave"></param>
        public bool SaveTo(string locationName, object infoToSave)
        {
            Add(locationName,infoToSave);
            return true;
        }

        /// <summary>
        /// use this to retrieve from a spot of your choice in Application Memory
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns></returns>
        public object GetFrom(string locationName)
        {

            return Retrieve(locationName);
        }

        public abstract bool ClearFor(string locationName);


    }
}