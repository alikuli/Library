using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using System;
using System.Reflection;
using UowLibrary.Interface;
using WebLibrary.Programs;

namespace UowLibrary.Abstract
{
    public abstract partial class AbstractBiz : IBiz
    {


        #region Memory Related
        public System.Web.HttpContextBase HttpContextBaseBiz
        {
            get
            {
                return _imemoryMain.HttpContextBase;
            }
        }

        public MemoryMain MemoryMain
        {
            get
            {
                if (_imemoryMain.IsNull())
                {
                    ErrorsGlobal.Add("Memory not initialized.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
                return (MemoryMain)_imemoryMain;
            }
        }

        public ApplicationMemory ApplicationMemory
        {
            get
            {
                return MemoryMain.ApplicationMemory;
            }
        }

        public SessionMemory SessionMemory
        {
            get
            {
                return MemoryMain.SessionMemory;
            }
        }

        public CacheMemory CacheMemory
        {
            get
            {
                return MemoryMain.CacheMemory;
            }
        }

        bool _isInitializedAlready;
        //once the program initializes, this is set to true. If this is false, then the program requires initialization
        public bool IsInitializedAlready
        {
            get
            {
                _isInitializedAlready = (bool)(CacheMemory.GetFrom("IsInitialized") ?? false);
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
                    ErrorsGlobal.AddError_EmptyString(MethodBase.GetCurrentMethod(), "IsInitialized");
                    CacheMemory.Add("IsInitialized", value);

                }

                //it is a valid boolean here....
                _isInitializedAlready = _isInitializedstr.ToBool();
                CacheMemory.Add("IsInitialized", _isInitializedAlready);


            }
        }

        #endregion



    }


}



