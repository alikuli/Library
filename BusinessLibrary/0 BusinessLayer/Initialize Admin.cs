using AliKuli.Extentions;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Reflection;
using UowLibrary.Abstract;
using UowLibrary.Interface;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {





    }
}
