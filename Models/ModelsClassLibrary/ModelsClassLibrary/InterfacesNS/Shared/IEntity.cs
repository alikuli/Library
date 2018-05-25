using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.Models.CommonAndShared
{
    /// <summary>
    /// This is the most basic Interface.
    /// </summary>
    public interface IEntity
    {
        string Id { get; set; }
    }
}
