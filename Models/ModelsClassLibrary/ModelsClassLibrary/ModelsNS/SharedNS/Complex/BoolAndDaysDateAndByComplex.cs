using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS.Complex
{
    [ComplexType]
    public class BoolAndDaysDateAndByComplex : DateAndByComplex
    {
        public bool Selected { get; set; }
        public int NoOfDays { get; set; }

        public void MarkTrue(int noOfDays, string userName, string userId)
        {
            Selected = true;
            NoOfDays = noOfDays;
            base.SetToTodaysDate(userName, userId);
        }
        public override void Clear()
        {
            Selected = false;
            NoOfDays = 0;
            Clear();
        }
    }
}
