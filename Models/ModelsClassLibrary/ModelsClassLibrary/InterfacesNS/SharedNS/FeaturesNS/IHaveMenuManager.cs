using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS
{
    public interface IHaveMenuManager

    {
        //MenuLevelENUM MenuLevelEnum { get; set; }
        //MenuPathMain MenuPathMain { get; set; }
        //Product Product { get; set; }
        //ProductChild ProductChild { get; set; }
        //string ReturnUrl { get; set; }
        IMenuManager MenuManager { get; set; }

    }
}
