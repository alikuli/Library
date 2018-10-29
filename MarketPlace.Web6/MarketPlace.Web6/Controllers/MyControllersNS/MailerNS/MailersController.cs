using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.MailerNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class MailersController : EntityAbstractController<Mailer>
    {

        MailerBiz _mailerBiz;
        public MailersController(MailerBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _mailerBiz = biz;
        }

        MailerBiz MailerBiz
        {
            get
            {
                return _mailerBiz;
            }
        }




        //public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        //{
        //    ViewBag.TrustLevelsSelectList = MailerBiz.SelectListTrustLevel;
        //    return base.Event_CreateViewAndSetupSelectList(parm);
        //}





        public ActionResult PrintVerificationLetters(string id)
        {
            try
            {

                id.IsNullOrWhiteSpaceThrowArgumentException("id");
                string downloadFileName = "Verification_Letter_" + DateTime.Now.Ticks.ToString() + ".pdf";
                FileContentResult fs =  File(MailerBiz.PrintVerificationLetters(id), "application/pdf", downloadFileName);
                return fs;

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Unable to continue.", MethodBase.GetCurrentMethod(), e);
                return RedirectToAction("Index", "Menus");
            }
        }


        /// <summary>
        /// This creates the dashboard
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateMailingList()
        {
            try
            {
                MailerVMForAssigningVerifList mv = MailerBiz.CreateMailerVMForAssigningVerifList();

                return View(mv);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Unable to continue.", MethodBase.GetCurrentMethod(), e);
                return RedirectToAction("Index", "Menus");
            }
        }


        [HttpPost]
        public ActionResult CreateMailingList(MailerVMForAssigningVerifList mv)
        {
            try
            {
                MailerBiz.CreatMailingList(mv);
                return View("YourMailingListHasBeenCreated");

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Unable to continue.", MethodBase.GetCurrentMethod(), e);
                return RedirectToAction("Index", "Menus");
            }
        }



        public ActionResult CreateAMailer()
        {
            //we meed a list of users.
            CreateMailerVM vm = MailerBiz.Factory_CreateMailerVm();

            vm.UserSelect = MailerBiz.SelectListUsers;


            return View(vm);
        }


        [HttpPost]
        public ActionResult CreateAMailer(CreateMailerVM vm)
        {

            try
            {
                MailerBiz.CreateAndSaveMailer(vm);
                return RedirectToAction("Index","Menus");
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Mailer not saved", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("CreateAMailer");
            }
        }


        //public MailerVMForAssigningVerifList CreateMailerVMForAssigningVerifList()
        //{
        //    UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");

        //    MailerVMForAssigningVerifList mv = _mailerBiz.CreateAssignMailingListModel(UserId);

        //    mv.Foreign_Courier_Verifications_Available = MailerBiz.Total_Foreign_Courier_Available().ToString();
        //    mv.Foreign_Postal_Verifications_Available = MailerBiz.Foreign_Postal_Verifications_Available().ToString();
        //    mv.Pakistan_Courier_Verifications_Available = MailerBiz.Pakistan_Courier_Verifications_Available().ToString();
        //    mv.Pakistan_Postal_Verifications_Available = MailerBiz.Pakistan_Postal_Verifications_Available().ToString();

        //    mv.MailerId = MailerBiz.GetMailerIdFor(UserId);
        //    mv.MailLocalOrForiegnEnum = MailLocalOrForiegnENUM.Unknown;
        //    mv.MailServiceEnum = MailServiceENUM.Unknown;
        //    mv.Total_Open_Mailings_For_Mailer = MailerBiz.Total_Open_Mailings_For_Mailer(mv.MailerId).ToString();
        //    return mv;
        //}

    }
}