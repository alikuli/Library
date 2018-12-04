
namespace UowLibrary
{
    /// <summary>
    /// This is where all the data initialization takes place. There is a default built version which accepts data as string[].
    /// To send data to this, override GetDataForStringArrayFormat;
    /// To send more complicated data, override GetData.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity>
    {

    }
}
