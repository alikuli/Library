using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestHdrNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.ServiceRequestHdrNS;
using UowLibrary.SuperLayerNS;


namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class ServiceRequestHdrsController : EntityAbstractController<ServiceRequestHdr>
    {

        ServiceRequestHdrBiz _serviceRequestHdrsBiz;
        SuperBiz _superBiz;


        public ServiceRequestHdrsController(AbstractControllerParameters param, SuperBiz superBiz)
            : base(superBiz.ServiceRequestHdrBiz, param)
        {
            _serviceRequestHdrsBiz = superBiz.ServiceRequestHdrBiz;
            _superBiz = superBiz;
        }





        SuperBiz SuperBiz
        {
            get
            {
                _superBiz.UserId = UserId;
                _superBiz.UserName = UserName;
                return _superBiz;
            }
        }
        ServiceRequestHdrBiz ServiceRequestHdrBiz
        {
            get
            {
                return _serviceRequestHdrsBiz;
            }
        }


        public ActionResult IWantTo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IWantTo(string button)
        {
            UserId.IsNullOrWhiteSpaceThrowException();
            switch (button)
            {
                case "salesman":

                    try
                    {
                        SuperBiz.CreateAnIWantToBeASalesmanEntry(UserId);
                        return View("SalesmanRequestSubmitted");

                    }
                    catch (Exception ex)
                    {
                        ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                    }

                    break;

                case "mailer":
                    break;

                case "seller":
                    break;

                case "deliveryman":
                    break;

                default:
                    break;
            }
            return RedirectToAction("IWantTo");
        }

        //public ActionResult IWantToBeASalesman()
        //{
        //    //create an entry so that it shows in the bulletin board
        //    UserId.IsNullOrWhiteSpaceThrowException();
        //    ServiceRequestHdrBiz.CreateAnIWantToBeASalesmanEntry(;
        //    return View("IWantTo");
        //}

        /// <summary>
        /// This is the person who has purchased this request and will teach the requester.
        /// </summary>
        public string InstructorId { get; set; }
        public Person Instructor { get; set; }

        [NotMapped]
        public SelectList SelectListInstructor { get; set; }
        public ActionResult ListOfPeopleWantingJobs()
        {
            List<ServiceRequestVM> lstOfppl = new List<ServiceRequestVM>();
            try
            {
                lstOfppl = SuperBiz.GetListOfPeopleWantingJobs();
                return View(lstOfppl);
            }
            catch (Exception e)
            {
                string err = string.Format("something went wrong.");
                ErrorsGlobal.Add(err, MethodBase.GetCurrentMethod(), e);
            }

            return View(lstOfppl);
        }

        public ActionResult PurchaseService(string serviceHeader, string returnUrl/*, BuySellDocStateModifierENUM buySellDocStateModifierEnum = BuySellDocStateModifierENUM.Unknown */)
        {
            throw new NotImplementedException();
            //try
            //{
            //    buySellDocId.IsNullOrWhiteSpaceThrowArgumentException("Id");
            //    returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");
            //    text.IsNullOrWhiteSpaceThrowArgumentException("text");


            //    if (buySellDocumentTypeEnum == BuySellDocumentTypeENUM.Unknown)
            //        throw new Exception("buySellDocumentType unknown");


            //    BuySellDoc buySellDoc = BuySellDocBiz.Find(buySellDocId);
            //    buySellDoc.IsNullThrowException();
            //    buySellDoc.BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
            //    buySellDoc.BuySellDocStateModifierEnum = buySellDocStateModifierEnum;
            //    //get the penalty amounts
            //    PersonPayingPenalty personPayingPenalty;
            //    IPenaltyClass penaltyClass = PenaltyController.GetPenalty(buySellDoc, out personPayingPenalty);

            //    if (!penaltyClass.IsNull())
            //    {
            //        text = penaltyClass.Text;
            //    }
            //    RejectCancelDeleteInbetweenClass rejectCancelDeleteInbetweenClass = new RejectCancelDeleteInbetweenClass(
            //        returnUrl,
            //        text,
            //        buySellDoc,
            //        GlobalObject);

            //    return View(rejectCancelDeleteInbetweenClass);
            //}
            //catch (Exception ex)
            //{
            //    ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
            //    return Redirect(returnUrl);
            //}
        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {

            setupSelectList(parm);
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            setupSelectList(parm);
            ServiceRequestHdr svh = ServiceRequestHdr.Unbox(parm.Entity);

            Person person = SuperBiz.PersonBiz.GetPersonForUserId(UserId);
            person.IsNullThrowException();
            svh.PersonFromId = person.Id;
            svh.PersonFrom = person;

            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }

        private void setupSelectList(ControllerIndexParams parm)
        {
            ServiceRequestHdr svh = ServiceRequestHdr.Unbox(parm.Entity);
            svh.SelectListPersonFrom = SuperBiz.PersonBiz.SelectList();
            svh.SelectListPersonTo = SuperBiz.PersonBiz.SelectList();
        }

        public decimal AmountPaid { get; set; }
        public decimal AmountReceived { get; set; }
    }
}