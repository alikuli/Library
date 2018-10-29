using System;

namespace InterfacesLibrary.SharedNS
{
    public interface IDateAndBy
    {
        DateTime? DateStart { get; set; }
        DateTime? Date { get; set; }
        string By { get; set; }

        void SetToTodaysDate(string byUser);
        void SetToTodaysDateStart(string byUser);
        void SetDateTo(string byUser, int noOfDays);

    }
}
