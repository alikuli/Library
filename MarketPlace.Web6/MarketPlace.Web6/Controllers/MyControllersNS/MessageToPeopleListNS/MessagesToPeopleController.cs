using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.MessagesToPeopleListNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.MessageNS;
using UowLibrary.PlayersNS.MessageToPeopleListNS;
using UowLibrary.PlayersNS.PersonNS;

namespace MarketPlace.Web6.Controllers
{
    public class MessagesToPeopleController : EntityAbstractController<MessageToPeopleList>
    {

        MessageBiz _messageBiz;
        PersonBiz _personBiz;
        public MessagesToPeopleController(MessageToPeopleListBiz biz, AbstractControllerParameters param, PersonBiz personBiz, MessageBiz messageBiz)
            : base(biz, param)
        {
            _messageBiz = messageBiz;
            _personBiz = personBiz;
        }

        PersonBiz PersonBiz
        {
            get
            {
                _personBiz.IsNullThrowException();
                _personBiz.UserId = UserId;
                _personBiz.UserName = UserName;
                return _personBiz;
            }
        }
        MessageBiz MessageBiz
        {
            get
            {
                _messageBiz.IsNullThrowException();
                _messageBiz.UserId = UserId;
                _messageBiz.UserName = UserName;
                return _messageBiz;
            }
        }


        public override System.Web.Mvc.ActionResult Event_Create_ViewAndSetupSelectList_GET(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {
            setUpSelectLists(parm);
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }

        private void setUpSelectLists(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {
            MessageToPeopleList MessageToPeopleList = MessageToPeopleList.Unbox(parm.Entity);
            MessageToPeopleList.SelectListPerson = PersonBiz.SelectList();
            MessageToPeopleList.SelectListMessage = MessageBiz.SelectList();
        }

        public override System.Web.Mvc.ActionResult Event_Edit_ViewAndSetupSelectList_GET(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {
            setUpSelectLists(parm);
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }


    }
}
