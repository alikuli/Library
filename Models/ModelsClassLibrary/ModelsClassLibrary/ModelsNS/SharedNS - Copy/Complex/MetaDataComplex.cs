using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using InterfacesLibrary.SharedNS;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
   [ComplexType]
   
    public class MetaDataComplex : MetaData, IMetaData
    {
    }
}