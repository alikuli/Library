using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.VehicalTypeNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.PlayersNS.VehicalTypeNS
{
    public partial class VehicalTypeBiz : BusinessLayer<VehicalType>
    {
        public VehicalTypeBiz(IRepositry<VehicalType> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }
        public override string SelectListCacheKey
        {

            get { return "SelectListCacheKeyVehicalType"; }
        }

        public void VehicalTypeDefaultItems()
        {
            createDefaultItem("Any Vehical Type", "Any vehical can carry the goods. Mr. Deliveryman please make your own judgement");
            createDefaultItem("Motorcycle", "Mr. Deliveryman, please send a motorcycle of bigger.");
            createDefaultItem("Car", "Mr. Deliveryman, you will need a car in my opinion.");
            createDefaultItem("Truck -Small", "Mr. Deliveryman, in my opinion, the small parcel requires a small truck!");
            createDefaultItem("Truck -Medium", "Mr. Deliveryman, in my opinion, the small parcel requires a medium truck!");
            createDefaultItem("Truck -Large", "Mr. Deliveryman, in my opinion, the small parcel requires a large truck!");
            createDefaultItem("Truck -Trailer", "Mr. Deliveryman, in my opinion, the small parcel requires a truck with a trailer!");
            createDefaultItem("Truck -Container", "Mr. Deliveryman, in my opinion, the small parcel requires a truck with a container!");
        }

        private void createDefaultItem(string name, string comment)
        {
            if (name.IsNullOrWhiteSpace())
                return;

            VehicalType vt = FindByName(name);
            if (vt.IsNull())
            {
                vt = Factory() as VehicalType;
                vt.Name = name.ToTitleCase();
                vt.Comment = comment;
                CreateAndSave(vt);

            }
        }


        public override void Event_ModifyIndexList(ModelsClassLibrary.ViewModels.IndexListVM indexListVM, ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parameters)
        {

            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.Show.EditDeleteAndCreate = true;
        }
    }
}
