using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UserNameSpace
{
    [ComplexType]
    public class UserActive
    {
        public UserActive()
        {
            Value = false;
            ActivateDate = new DateAndByComplex();
            DeactivateDate = new DateAndByComplex();

        }
        public bool Value { get; set; }

        [Display(Name = "Activate Date")]
        public DateAndByComplex ActivateDate { get; set; }

        [Display(Name = "Deactivate Date")]
        public DateAndByComplex DeactivateDate { get; set; }

        public void Activate(string userId)
        {
            Value = true;
            ActivateDate.SetToTodaysDate(userId);
        }
        public void Deactivate(string userId)
        {
            Value = false;
            DeactivateDate.SetToTodaysDate(userId);
        }
    }
}
