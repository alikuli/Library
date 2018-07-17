using System;
using ErrorHandlerLibrary.ExceptionsNS;
namespace MarketPlace.Web6.Controllers.Abstract
{
    interface IAbstractController
    {
        void AddErrorsIntoModelState();
        ErrorSet ErrorsGlobal { get; }
    }
}
