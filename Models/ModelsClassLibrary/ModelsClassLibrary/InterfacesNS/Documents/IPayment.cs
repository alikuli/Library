using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.Interfaces.Documents
{
    public interface IPayment
    {
        decimal TotalAmount { get; set; }

    }
}
