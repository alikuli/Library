using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using System.Linq;

namespace ModelsClassLibrary.CashNS.PenaltyNS

{
    public partial class PenaltyHeaderBiz: BusinessLayer<PenaltyHeader>
    {
        PenaltyTrxBiz _penaltyTrxBiz;
        public PenaltyHeaderBiz(IRepositry<PenaltyHeader> entityDal, BizParameters bizParameters, PenaltyTrxBiz penaltyTrxBiz)
            : base(entityDal, bizParameters)
        {
            _penaltyTrxBiz = penaltyTrxBiz;
        }


        public PenaltyTrxBiz PenaltyTrxBiz
        {
            get
            {
                _penaltyTrxBiz.UserId = UserId;
                _penaltyTrxBiz.UserName = UserName;
                return _penaltyTrxBiz;
            }
        }

        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            PenaltyHeader penaltyHeader = PenaltyHeader.Unbox(parm.Entity);
            addDocumentNumber(penaltyHeader);
            fixName(penaltyHeader);

        }

        private void addDocumentNumber(PenaltyHeader penaltyHeader)
        {
            if (penaltyHeader.DocumentNo == 0)
            {
                List<PenaltyHeader> lst = FindAll().ToList();
                long docNo = 1;
                if (lst.IsNullOrEmpty())
                { }
                else
                {
                    docNo = lst.Max(x => x.DocumentNo) + 1;
                }
                penaltyHeader.DocumentNo = docNo;
            }
        }

        private void fixName(PenaltyHeader penaltyHeader)
        {
            penaltyHeader.Name = penaltyHeader.MakeUniqueName();
        }

        /// <summary>
        /// The penalty header needs to be saved outside this
        /// </summary>
        /// <param name="penaltyHeader"></param>
        /// <param name="penaltyTrx"></param>
        public void AddPenaltyTrx(PenaltyHeader penaltyHeader, PenaltyTrx penaltyTrx)
        {
            penaltyHeader.IsNullThrowException();
            penaltyTrx.IsNullThrowException();

            if (penaltyTrx.Amount == 0)
                return;

            if (penaltyHeader.PenaltyTrxs.IsNull())
                penaltyHeader.PenaltyTrxs = new List<PenaltyTrx>();

            penaltyHeader.PenaltyTrxs.Add(penaltyTrx);
            penaltyTrx.PenaltyHeaderId = penaltyHeader.Id;

            PenaltyTrxBiz.Create(penaltyTrx);

        }
    }
}
