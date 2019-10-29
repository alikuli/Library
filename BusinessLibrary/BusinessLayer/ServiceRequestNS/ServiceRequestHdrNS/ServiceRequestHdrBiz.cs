using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.VerificationNS;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestHdrNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.BusinessLayer.ServiceRequestNS.ServiceRequestHdrNS;
//using UowLibrary.BusinessLayer.ServiceRequestNS.ServiceRequestTrxNS;
using UowLibrary.ParametersNS;
//using UowLibrary.PlayersNS.ServiceRequestTrxNS;

namespace UowLibrary.PlayersNS.ServiceRequestHdrNS
{
    public partial class ServiceRequestHdrBiz : BusinessLayer<ServiceRequestHdr>, IServiceRequestHdrBiz
    {
        //IServiceRequestTrxBiz _iserviceRequestTrxBiz;
        public ServiceRequestHdrBiz(IRepositry<ServiceRequestHdr> entityDal, BizParameters bizParameters /*, IServiceRequestTrxBiz iserviceRequestTrxBiz */)
            : base(entityDal, bizParameters)
        {
            //_iserviceRequestTrxBiz = iserviceRequestTrxBiz;
        }

        public static ServiceRequestHdrBiz Unbox(IServiceRequestHdrBiz iserviceRequestHdrBiz)
        {
            ServiceRequestHdrBiz serviceRequestHdrBiz = iserviceRequestHdrBiz as ServiceRequestHdrBiz;
            serviceRequestHdrBiz.IsNullThrowException();
            return serviceRequestHdrBiz;

        }

        //public ServiceRequestTrxBiz ServiceRequestTrxBiz
        //{
        //    get
        //    {
        //        _iserviceRequestTrxBiz.UserId = UserId;
        //        _iserviceRequestTrxBiz.UserName = UserName;
        //        return ServiceRequestTrxBiz.Unbox(_iserviceRequestTrxBiz);
        //    }
        //}


        public override string SelectListCacheKey
        {

            get { return "SelectListCacheKeyServiceRequestHdr"; }
        }



        public override void Event_ModifyIndexList(ModelsClassLibrary.ViewModels.IndexListVM indexListVM, ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parameters)
        {

            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.Show.EditDeleteAndCreate = true;
        }

        public override void Fix(ControllerCreateEditParameter parm)
        {
            ServiceRequestHdr srh = ServiceRequestHdr.Unbox(parm.Entity);
            fixName(srh);
            base.Fix(parm);
            addDocumentNumber(srh);

        }

        private void fixName(ServiceRequestHdr srh)
        {
            if (srh.Name.IsNullOrWhiteSpace())
                srh.Name = srh.MakeUniqueName();
        }

        private void addDocumentNumber(ServiceRequestHdr serviceRequestHdr)
        {
            if (serviceRequestHdr.DocumentNo != 0)
                return;

            List<ServiceRequestHdr> lst = FindAll().ToList();

            long docNo = 1;
            if (lst.IsNullOrEmpty())
            { }
            else
            {
                docNo = lst.Max(x => x.DocumentNo) + 1;
            }
            serviceRequestHdr.DocumentNo = docNo;

        }


        public void CreateAnIWantToBeASalesmanEntry(string userPersonId, string systemPersonId, decimal amount)
        {
            ServiceRequestHdr svh = new ServiceRequestHdr(userPersonId, amount, ServiceRequestTypeENUM.BecomeSalesman);
            //check to see if an older request does not exist

            ServiceRequestHdr svhExists = FindAll()
                .FirstOrDefault(x => 
                    x.PersonFromId == userPersonId && 
                    x.RequestTypeEnum == ServiceRequestTypeENUM.BecomeSalesman &&
                    x.ServiceRequestStatusEnum == EnumLibrary.EnumNS.ServiceRequestStatusENUM.Open);

            if (svhExists.IsNull())
            {
                CreateAndSave(svh);
            }
            else
            {
                string err = string.Format("A request by you already exists. It was submited on {0}.", svhExists.MetaData.Created.Date_NotNull_Min.ToShortDateString());
                //create some kind of an entry to tell me that this error is occouring so I can take action to make sure the person is contacted.

                throw new Exception(err);
            }
        }



    }
}
