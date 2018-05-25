using System.Web;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using Ninject;

namespace WebLibrary.Programs
{
    public class MemoryMain : WebLibrary.Programs.IMemoryMain
    {
        private readonly System.Web.HttpContextBase _httpCtx;


        /// <summary>
        /// Send in the httpContext
        /// </summary>
        /// <param name="httpCtx"></param>
        [Inject]
        public MemoryMain(HttpContextBase httpCtx)
        {
            _httpCtx = httpCtx;

        }

        public HttpContextBase HttpContextBase
        {
            get
            {
                return _httpCtx;
            }
        }

        ApplicationMemory _applicationMemory;
        public ApplicationMemory ApplicationMemory
        {
            get { return _applicationMemory ?? (_applicationMemory = new ApplicationMemory(_httpCtx)); }
        }


        CacheMemory _cacheMemory;
        public CacheMemory CacheMemory
        {
            get { return _cacheMemory ?? (_cacheMemory = new CacheMemory(_httpCtx)); }
        }


        SessionMemory _sessionMemory;
        public SessionMemory SessionMemory
        {
            get { return _sessionMemory ?? (_sessionMemory = new SessionMemory(_httpCtx)); }
        }

        bool _isInitializedAlready;
        string isInitializedLocation = "IsInitialized";
        public bool IsInitializedAlready
        {
            get
            {
                _isInitializedAlready = (bool)(CacheMemory.GetFrom(isInitializedLocation) ?? false);
                return _isInitializedAlready;
            }
            set
            {

                if (value.IsNull())
                {
                    _isInitializedAlready = false;
                    CacheMemory.Add("IsInitialized", false);
                }

                string _isInitializedstr = value.ToString();

                if (_isInitializedstr.IsNullOrWhiteSpace() || !_isInitializedstr.IsValidBoolean())
                {
                    CacheMemory.Add(isInitializedLocation, false);//default

                }
                //it is a valid boolean here....
                _isInitializedAlready = _isInitializedstr.ToBool();
                CacheMemory.Add(isInitializedLocation, _isInitializedAlready);


            }
        }

    }
}
