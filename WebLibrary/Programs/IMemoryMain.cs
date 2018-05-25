using System.Web;
using AliKuli.UtilitiesNS;
namespace WebLibrary.Programs
{
    public interface IMemoryMain
    {
        HttpContextBase HttpContextBase {get;}
        ApplicationMemory ApplicationMemory { get; }
        CacheMemory CacheMemory { get; }
        SessionMemory SessionMemory { get; }
    }
}
