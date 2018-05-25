﻿using ModelsClassLibrary.ModelsNS.PlacesNS;
using System.Web.Mvc;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {



        public SelectList CountrySelectList
        {
            get
            {
                return CountryDal.SelectList();
            }
        }



        public override string SelectListCacheKey
        {
            get { return "StateSelectListData"; }
        }
    }
}
