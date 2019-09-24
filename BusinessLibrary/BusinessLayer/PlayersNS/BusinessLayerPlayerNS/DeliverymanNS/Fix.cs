using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.PlayersNS.DeliverymanNS
{
    public partial class DeliverymanBiz
    {


        //public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        //{
        //    base.Event_ModifyIndexList(indexListVM, parameters);
        //    indexListVM.Show.EditDeleteAndCreate = true;

        //}


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);

            Deliveryman deliveryman = parm.Entity as Deliveryman;

            if (deliveryman.DeliverymanCategoryId.IsNullOrWhiteSpace())
                deliveryman.DeliverymanCategoryId = null;

            if(deliveryman.Name.IsNullOrWhiteSpace())
            {
                if (deliveryman.Person.IsNull())
                {
                    deliveryman.PersonId.IsNullOrWhiteSpaceThrowException();
                    deliveryman.Person = PersonBiz.Find(deliveryman.PersonId);
                    deliveryman.Person.IsNullThrowException();
                }

                deliveryman.Name = deliveryman.Person.Name;

            }

        }

    }
}
