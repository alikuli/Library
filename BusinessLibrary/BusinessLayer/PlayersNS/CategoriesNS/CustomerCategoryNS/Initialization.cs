using System;
using System.Reflection;
using System.Threading.Tasks;
using AliKuli.Extentions;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ViewModels;
using WebLibrary.Programs;
using DatastoreNS;

namespace UowLibrary.PlayersNS.CustomerCategoryNS
{
    public partial class CustomerCategoryBiz : BusinessLayer<CustomerCategory>
    {



        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return CustomerCategoryData.DataArray();
            }
        }




    }
}
