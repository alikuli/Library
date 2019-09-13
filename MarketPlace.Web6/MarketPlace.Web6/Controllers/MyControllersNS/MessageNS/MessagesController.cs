using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.MessagesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.MessageNS;

namespace MarketPlace.Web6.Controllers
{
    public class MessagesController : EntityAbstractController<Message>
    {

        MessageBiz _messageBiz;
        public MessagesController(MessageBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _messageBiz = biz;
        }

        UserBiz UserBiz
        {
            get
            {
                return _messageBiz.UserBiz;
            }
        }

        MessageBiz MessageBiz
        {
            get
            {

                return _messageBiz;
            }
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            Message message = parm.Entity as Message;
            message.IsNullThrowException("Unable to unbox");
            message.SelectListPersonFrom = _messageBiz.PersonBiz.SelectList();

            if (!parm.ReturnUrl.IsNullOrWhiteSpace())
            {
                message.MenuManager.ReturnUrl = parm.ReturnUrl;
            }

            return base.Event_Create_ViewAndSetupSelectList_GET(parm);

        }


        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            Message message = parm.Entity as Message;
            message.IsNullThrowException("Unable to unbox");
            message.SelectListPersonFrom = _messageBiz.PersonBiz.SelectList();

            if (!parm.ReturnUrl.IsNullOrWhiteSpace())
            {
                message.MenuManager.ReturnUrl = parm.ReturnUrl;
            }

            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }

        [HttpGet]
        public ActionResult SaleMessage(string menuPathMainId, string productId, string productChildId, string returnUrl, ICollection<ProductChild> productChildrenBeingAdvertised, MessageENUM messageEnum = MessageENUM.Unknown, MenuENUM menuEnum = MenuENUM.Unknown)
        {



            try
            {
                //user must be logged in to send a message
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");

                if (messageEnum == MessageENUM.Unknown)
                    throw new Exception("messageEnum is unknown");

                if (menuEnum == MenuENUM.Unknown)
                    throw new Exception("menuEnum is unknown");

                //note this assumes current user is the sender.
                MessageParameter messageParameter = MessageBiz.GetPeopleListCount(menuPathMainId, productId, productChildId, menuEnum);
                messageParameter.ReturnUrl = returnUrl;
                return View(messageParameter);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Menus");
            }

        }



        [HttpPost]
        public ActionResult SaleMessage(MessageParameter messageParameter)
        {
            try
            {
                messageParameter.IsNullThrowExceptionArgument("messageParameter");
                MessageBiz.CreateMessageAndSave(messageParameter);
                return View("MessageSentConfirmation");
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
            }
            return Redirect(messageParameter.ReturnUrl);
        }



    }
}
