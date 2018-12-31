using AliKuli.Extentions;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebLibrary.Programs;

namespace UowLibrary.DiscountPrecedenceNS
{
    public partial class DiscountPrecedenceBiz : BusinessLayer<DiscountPrecedence>
    {


        public override string SelectListCacheKey
        {
            get { return "DiscountPrecedencesSelectListData"; }
        }


    }
}
