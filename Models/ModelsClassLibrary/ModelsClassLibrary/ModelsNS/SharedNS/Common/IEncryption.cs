namespace ModelsClassLibrary.ModelsNS.SharedNS.Common
{
    public interface IEncryption
    {
        string GetCreatedTicks { get; }
        bool IsEncrypted { get;  }
    }
}
