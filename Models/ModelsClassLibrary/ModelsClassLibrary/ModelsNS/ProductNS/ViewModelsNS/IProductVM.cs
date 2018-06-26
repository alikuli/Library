namespace ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels
{
    public interface IProductVM 
    {
        string MakeName();
        //bool IsMakeNameTitleSentance { get; set; }
        void SaveNameFields();
        void RestoreNameFields();
    }
}
