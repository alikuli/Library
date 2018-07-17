using InterfacesLibrary.ProductNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.MenuNS
{
    public partial class MenuPathMain 
    {


        [Display(Name = "Menu Path 1")]
        public virtual string MenuPath1Id { get; set; }
        public virtual MenuPath1 MenuPath1 { get; set; }


        [Display(Name = "Menu Path 2")]

        public virtual string MenuPath2Id { get; set; }
        public virtual MenuPath2 MenuPath2 { get; set; }


        [Display(Name = "Menu Path 3")]
        public virtual string MenuPath3Id { get; set; }
        public virtual MenuPath3 MenuPath3 { get; set; }


    }
}