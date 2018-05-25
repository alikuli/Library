
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    /// <summary>
    /// This object is used to transport message for the ErrorCustom view.
    /// </summary>
    public class ErrorObjHttp
    {

        public string Message { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", Message);
        }
    }
}