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

namespace UowLibrary.PlayersNS.DeliverymanCategoryNS
{
    public partial class DeliverymanCategoryBiz : BusinessLayer<DeliverymanCategory>
    {



        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                throw new NotImplementedException();
                //return DeliverymanCategoryData.DataArray();
            }
        }




    }
}
