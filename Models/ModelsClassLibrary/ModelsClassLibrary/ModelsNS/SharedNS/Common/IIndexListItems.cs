
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    /// <summary>
    /// This was made to help create the description line in the index method for the web pages in the index.
    /// This can be overridden and different fields from the model can be added. These are further manipulated in
    /// IndexItemVM to make one single line. Moreover, they are also used in the Repository to make the Index.
    /// Now you can control the description in the index from each model that implements this by just overriding Input1, Input2 and Input3
    /// </summary>
    public interface IIndexListItems
    {
        string Input1SortString { get; }
        string Input2SortString { get; }
        string Input3SortString { get; }
        //string NameInput1 { get; }
        //string NameInput2 { get; }
        //string NameInput3 { get; }
    }
}
