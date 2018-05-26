namespace ModelsClassLibrary.ModelsNS.ProductNS.UOM
{
    public interface IUom
    {
        double NoOfBaseUnits(double value);
        double UnitsToMakeOneOfBase { get; set; }
    }
}
