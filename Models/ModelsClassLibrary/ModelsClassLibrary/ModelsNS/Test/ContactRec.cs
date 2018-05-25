using ModelsClassLibrary.ModelsNS.CommonAndSharedNS;
using ModelsClassLibrary.ModelsNS.People;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS
{
    /// <summary>
    /// This stores how many contacts/pings a serviceman is given.
    /// </summary>
    public class ContactRec:CommonWithId
    {
        public ContactRec()
        {
            MethodOfContactEnum = MethodOfContactEnum.Unknown;
        }
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Created Start (UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public  DateTime? DateContacted { get; set; }

        [Display(Name = "Contacted By")]
        public MethodOfContactEnum MethodOfContactEnum { get; set; }
        public virtual Salesman Salesman{ get; set; }
    }
}