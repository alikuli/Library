
using ErrorHandlerLibrary.ExceptionsNS;
namespace UowLibrary.Interface
{
    public interface IBiz
    {
        ErrorSet ErrorsGlobal { get; }
        string UserId { get; set; }
        string UserName { get; set; }
    }
}
