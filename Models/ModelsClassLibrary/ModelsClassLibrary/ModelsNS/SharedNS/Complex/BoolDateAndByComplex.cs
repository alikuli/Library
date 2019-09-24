using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS.Complex
{
    [ComplexType]
    public class BoolDateAndByComplex : DateAndByComplex
    {
        public bool IsSelected { get; set; }

        public void MarkTrue(string userName, string userId)
        {
            SetToTodaysDate(userName, userId);
        }
        public override void Clear()
        {
            IsSelected = false;
            base.Clear();
        }
        public override void SetToTodaysDate(string byUser, string byUserId)
        {
            IsSelected = true;
            base.SetToTodaysDate(byUser, byUserId);
        }
    }
}
