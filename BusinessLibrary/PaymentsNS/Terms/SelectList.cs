using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;

namespace UowLibrary.PaymentTermNS
{
    public partial class PaymentTermBiz : BusinessLayer<PaymentTerm>
    {

        #region SelectList


        public override string SelectListCacheKey
        {
            get { return "PaymentTermsSelectListData"; }
        }
        /// <summary>
        ///// This loads the PaymentTerm Data into the select list
        ///// </summary>
        ///// <returns></returns>
        //public override SelectList SelectList()
        //{
        //    SelectList selectList = GetSelectListFromCache();
        //    if (selectList.IsNull())
        //    {

        //        //Load the data
        //        selectList = LoadDataIntoDbAndThenGetSelectListAndAddToCache();

        //    }
        //    return selectList;
        //}

        //string _key = "PaymentTermsSelectListData";
        //private SelectList GetSelectListFromCache()
        //{

        //    object obj = MemoryMain.CacheMemory.GetFrom(_key);
        //    ErrorsGlobal.AddMessage("Payment Terms Select List DAL has been Accessed.", MethodBase.GetCurrentMethod());
        //    return (SelectList)obj;
        //}

        //private SelectList LoadDataIntoDbAndThenGetSelectListAndAddToCache()
        //{
        //    ErrorsGlobal.AddMessage("PaymentTerms Select List DAL has been refilled because it was NOT in CACHE.", MethodBase.GetCurrentMethod());
        //    LoadIntoDb();

        //    var selectListData = SelectList();
        //    if (selectListData.IsNull())
        //    {
        //        ErrorsGlobal.Add("No Data loaded. Please load PaymentTerms data.", MethodBase.GetCurrentMethod());
        //        //return null;
        //    }

        //    MemoryMain.CacheMemory.Add(_key, selectListData, new System.TimeSpan(10, 0, 0, 0));
        //    object obj = MemoryMain.CacheMemory.GetFrom(_key);
        //    return (SelectList)obj;
        //}


        //private void LoadIntoDb()
        //{
        //    //now make sure that PaymentTerm has been loaded.
        //    var allCountryDataInDb = Dal.FindAll().ToList();

        //    if (allCountryDataInDb.IsNullOrEmpty())
        //    {
        //        InitializationData();
        //    }

        //}


        #endregion



    }
}
