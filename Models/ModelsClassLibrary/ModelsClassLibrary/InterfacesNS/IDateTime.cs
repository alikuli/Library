using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.Interfaces
{
    interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}
