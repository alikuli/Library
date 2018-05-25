namespace WebLibrary
{
    public interface IMemory
    {
        System.Web.HttpContext HttpCtx { get; }
        bool ClearFor(string locationName);
        object Memory { get; set; }
        bool Clear();
        object GetFrom(string locationName);
        bool SaveTo(string locationName, object infoToSave);
        //IErrorSet Errors { get; }
    }
}
