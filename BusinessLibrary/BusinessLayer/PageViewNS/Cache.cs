using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InvoiceNS;
using MigraDocLibrary;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using System.Linq;
using ErrorHandlerLibrary;
using ModelsClassLibrary.ModelsNS.DashBoardNS;
using System;
using System.Collections.Generic;

namespace UowLibrary.PageViewNS
{
    public partial class PageViewBiz 
    {

        CacheMemory Cache
        {
            get
            {
                return MemoryMain.CacheMemory;
            }
        }



        void saveToCache(List<PageView> lst, string location)
        {
            TimeSpan tsp = new TimeSpan(0, 0, 5);
            Cache.Add(location, lst, tsp);
        }



        List<PageView> getFromCache(string location)
        {
            List<PageView> lst = Cache.GetFrom(location) as List<PageView>;
            return lst;
        }
        

    }
}
