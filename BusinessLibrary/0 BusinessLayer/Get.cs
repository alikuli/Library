using AliKuli.ToolsNS;
using InterfacesLibrary.SharedNS;
using System.Web;
using UowLibrary.Abstract;
using UowLibrary.Interface;
namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {




        public virtual string GetClassName()
        {
            return Dal.GetClassName;
        }



        public string GetMappedPath(string relativePath)
        {
            //return HttpContext.Current.Server.MapPath(relativePath);
            return FileTools.GetPath(relativePath);
        }



    }
}
