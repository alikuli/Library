using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS.Complex
{
    [ComplexType]
    public class BoolDateAndByComplex : DateAndByComplex
    {
        public bool Selected { get; set; }

        public void MarkTrue(string userName, string userId)
        {
            Selected = true;
            base.SetToTodaysDate(userName, userId);
        }
        public override void Clear()
        {
            Selected = false;
            Clear();
        }
    }
}
