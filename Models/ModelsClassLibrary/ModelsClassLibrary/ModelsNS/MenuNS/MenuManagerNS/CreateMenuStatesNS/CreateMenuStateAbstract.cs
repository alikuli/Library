using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS
{
    public abstract class CreateMenuStateAbstract : ICreateMenuState
    {
        public virtual bool Disable_MenuPath1
        {
            get{return false;}
        }
        public virtual bool Disable_MenuPath2
        {
            get{return false;}
        }
        public virtual bool Disable_MenuPath3
        {
            get{return false;}
        }

    }
}
