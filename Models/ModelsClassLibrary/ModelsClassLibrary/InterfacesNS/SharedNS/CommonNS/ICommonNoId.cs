using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.Models.Shared
{

    /// <summary>
    /// This interface is used so that we can make one single select list program that will work with everthing
    /// </summary>
    public interface ICommonNoId:ICommonNoIdBasics
    {
        bool Active { get; set; }
        string Comment { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime CreatedDateStarted { get; set; }
        string CreatedUser { get; set; }
        bool Deleted { get; set; }
        DateTime? DeleteDate { get; set; }
        string DeletedByUser { get; set; }
        bool Equals(object obj);
        string IdString();
        bool IsAutoCreated { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime? ModifiedDateStart { get; set; }
        string ModifiedUser { get; set; }
        //void SelfErrorCheck();
        DateTime? UnDeleteDate { get; set; }
        string UnDeletedByUser { get; set; }
        //string GetSelfClassName();
        void LoadFrom(ICommonNoId i);



    }
}
