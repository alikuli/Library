using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using System;
namespace ModelsClassLibrary.ModelsNS.ProductNS.ProductNS
{
    public interface IProductAutomobileVM: IProduct
    {
        AutomobileGearTypeENUM AutomobileGearTypeEnum { get; set; }
        string AutomobileGearTypeEnumToString { get; }
        string Brand { get; set; }
        string EngineSize { get; set; }
        FuelTypeENUM FuelTypeEnum { get; set; }
        string FuelTypeEnumToString { get; }
        string ModelNumber { get; set; }
        int NumberOfSeats { get; set; }
        void RestoreNameFields();
        //void saveNameFields();
        //void SaveName();
    }
}
