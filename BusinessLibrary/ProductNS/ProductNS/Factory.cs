
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Linq;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {

        /// <summary>
        /// We need to do this because we need to create the VM for Create, otherwise it is very difficult to transfer info. This appears
        /// to be cleaner... lets see.
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        //public override ICommonWithId Factory()
        //{
        //    if (fp.MenuLevelEnum == MenuLevelENUM.Level_1)
        //        return Dal.Factory() as ICommonWithId;

        //    MenuPathMain mpm = _menuPathMainBiz.Find(fp.MenuPathMainId);
        //    mpm.IsNullThrowException("Menu Path Main not found.");

        //    switch (mpm.MenuPath1.MenuPath1Enum)
        //    {
        //        case MenuPath1ENUM.Unknown:
        //            break;

        //        case MenuPath1ENUM.Automobiles:
        //            ProductAutomobileVM productAutomobileVM = new ProductAutomobileVM();
        //            productAutomobileVM.MenuManager.MenuPathMain = mpm;
        //            return productAutomobileVM as ICommonWithId;

        //        case MenuPath1ENUM.MensClothing:
        //            break;
        //        case MenuPath1ENUM.WomensClothing:
        //            break;
        //        case MenuPath1ENUM.Electronics:
        //            break;
        //        case MenuPath1ENUM.Foods:
        //            break;
        //        case MenuPath1ENUM.HomeServants:
        //            break;
        //        case MenuPath1ENUM.FactoryWorkers:
        //            break;
        //        case MenuPath1ENUM.OfficeWorkers:
        //            break;
        //        case MenuPath1ENUM.Machines:
        //            break;
        //        case MenuPath1ENUM.Stationary:
        //            break;
        //        case MenuPath1ENUM.FruitProccessors:
        //            break;
        //        case MenuPath1ENUM.Steel:
        //            break;
        //        case MenuPath1ENUM.Cement:

        //            break;
        //        case MenuPath1ENUM.Electricity:
        //            break;
        //        default:
        //            break;
        //    }
        //    return base.Factory(fp);
        //}
    }
}
