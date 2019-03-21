using System;

namespace InterfacesLibrary.SharedNS
{
    public interface IDateAndBy
    {
        DateTime? DateStart { get; set; }
        DateTime? Date { get; set; }
        string By { get; set; }

        void SetToTodaysDate(string byUser, string byUserId);
        void SetToTodaysDateStart(string byUser, string byUserId);
        void SetDateTo(string byUser, int noOfDays);

    }
}
