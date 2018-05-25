using AliKuli.Extentions;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;


namespace DalLibrary.DalNS
{

    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        public ApplicationDbContext GetDb
        {
            get { return _db; }
        }



        public string GetSelfClassName()
        {
            TEntity entity = Factory();
            return entity.MetaData.GetSelfClassName();

        }

        public string GetSelfMethodName()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            var fullName = string.Format("{0}.{1}({2})", method.ReflectedType.FullName, method.Name, string.Join(",", method.GetParameters().Select(o => string.Format("{0} {1}", o.ParameterType, o.Name)).ToArray()));

            return fullName;
        }

        public virtual string GetClassName
        {
            get
            {
                Type t = typeof(TEntity);
                return t.Name.ToTitleCase();
            }
        }



    }
}
