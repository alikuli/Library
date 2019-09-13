using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.VehicalTypeNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.VehicalTypeNS;
namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class VehicalTypesController : EntityAbstractController<VehicalType>
    {

        VehicalTypeBiz _vehicalTypeBiz;
        public VehicalTypesController(VehicalTypeBiz vehicalTypeBiz, AbstractControllerParameters param)
            : base(vehicalTypeBiz, param)
        {
            _vehicalTypeBiz = vehicalTypeBiz;
        }


        VehicalTypeBiz VehicalTypeBiz
        {
            get
            {
                return _vehicalTypeBiz;
            }
        }
        public override ActionResult InitializeDb()
        {
            //add the "Any"item
            //check to see if any Exists

            VehicalTypeBiz.VehicalTypeDefaultItems();
            return RedirectToAction("Index");
        }

        //private void VehicalTypeDefaultItems()
        //{
        //    createDefaultItem("Any");
        //    createDefaultItem("Motorcycle");
        //    createDefaultItem("Car");
        //    createDefaultItem("Truck -Small");
        //    createDefaultItem("Truck -Medium");
        //    createDefaultItem("Truck -Large");
        //    createDefaultItem("Truck -Trailer");
        //    createDefaultItem("Truck -Container");
        //}

        //private void createDefaultItem(string name)
        //{
        //    if (name.IsNullOrWhiteSpace())
        //        return;

        //    VehicalType vt = VehicalTypeBiz.FindByName(name);
        //    if (vt.IsNull())
        //    {
        //        vt = VehicalTypeBiz.Factory() as VehicalType;
        //        vt.Name = name.ToTitleCase();
        //        VehicalTypeBiz.CreateAndSave(vt);

        //    }
        //}


    }
}