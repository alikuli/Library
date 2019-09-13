using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.MessagesNS;
using ModelsClassLibrary.ModelsNS.MessagesToPeopleListNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace UowLibrary.PlayersNS.MessageToPeopleListNS
{
    public partial class MessageToPeopleListBiz : BusinessLayer<MessageToPeopleList>
    {

        //readonly PersonBiz _personBiz;
        //readonly ProductBiz _productBiz;
        //readonly OwnerBiz _ownerBiz;
        //readonly PeopleMessageBiz _peopleMessageBiz;
        public MessageToPeopleListBiz(IRepositry<MessageToPeopleList> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {
        }




    }
}
