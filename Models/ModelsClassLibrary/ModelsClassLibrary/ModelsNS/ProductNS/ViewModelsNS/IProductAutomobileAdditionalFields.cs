using System;
namespace ModelsClassLibrary.ModelsNS.ProductNS.ViewModelsNS.ProductAutomobileNS
{
    public interface IAdditionalFields
    {
        void Deserialize(string additionalFieldsString);
        string Serialize();
        string ToString();
    }
}
