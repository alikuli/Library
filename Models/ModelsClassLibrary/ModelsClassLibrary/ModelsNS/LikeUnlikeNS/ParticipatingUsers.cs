using AliKuli.Extentions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [NotMapped]
    public class ParticipatingUsers
    {
        public ParticipatingUsers()
        {

        }
        /// <summary>
        /// this class is used for showing the users who have comments, clicked etc
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="imageLocation"></param>
        public ParticipatingUsers(string id, string name, string imageLocation, string userComment, DateTime createDate)
        {
            Id = id;
            Name = name;
            ImageLocation = imageLocation;
            UserComment = userComment;
            CreateDate = createDate;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageLocation { get; set; }
        public string UserAddressFixed { get; set; }
        public string UserComment { get; set; }
        public DateTime CreateDate { get; set; }
        public string NameWithDateTime
        {

            get
            {
                if (CreateDate == DateTime.MaxValue)
                    return string.Format("{0:0} {1}", "Unknown", Name);


                TimeSpan noOfDaysAgoTimeSpan = DateTime.UtcNow.Subtract(CreateDate);
                int daysAgo = noOfDaysAgoTimeSpan.Days;
                int hoursAgo = noOfDaysAgoTimeSpan.Hours;
                int minAgo = noOfDaysAgoTimeSpan.Minutes;
                int secAgo = noOfDaysAgoTimeSpan.Seconds;
                int weeksAgo = noOfDaysAgoTimeSpan.ToWeeks().ToString().ToInt();
                int monthsAgo = noOfDaysAgoTimeSpan.ToMonths().ToString().ToInt();
                int yearsAgo = noOfDaysAgoTimeSpan.ToWeeks().ToString().ToInt();



                int monthsAgoWithoutAnyYears = (noOfDaysAgoTimeSpan.ToMonths() % 12).ToString().ToInt();
                int yrsAgo = noOfDaysAgoTimeSpan.ToYears().ToString().ToInt();
                int weeksAgoWithoutMonths = (noOfDaysAgoTimeSpan.TotalDays / 7).ToString().ToInt();
                int daysAgoWithoutWeeks = ((noOfDaysAgoTimeSpan.TotalDays % 7).ToString().ToInt()) * 7;
                StringBuilder sb = new StringBuilder();
                //sb.Append(string.Format("[{0:0} {1}] ", CreateDate.ToLongDateString(),CreateDate.ToLongTimeString()));
                sb.Append(Name + " said ");


                if (daysAgo == 0)
                {
                    sb.Append(getDaysString(daysAgo));
                    sb.Append(getHoursString(hoursAgo));
                    sb.Append(getMinutesString(minAgo));
                    sb.Append(getSecondsString(secAgo));

                    if (hoursAgo > 0)
                    {
                        sb.Append("ago");
                        return sb.ToString();
                    }
                    if (minAgo > 0)
                    {
                        sb.Append("ago");
                        return sb.ToString();
                    }
                    if (secAgo > 0)
                    {
                        sb.Append("ago");
                        return sb.ToString();
                    }
                    sb.Append("just now");
                    return sb.ToString();
                }

                if (daysAgo < 7)
                {
                    sb.Append(getDaysString(daysAgo));
                    if (daysAgo > 1)
                        sb.Append("ago");

                    return sb.ToString().Trim();

                }
                if (daysAgo < 29)
                {
                    sb.Append(getWeekString(weeksAgo));
                    if (weeksAgo > 1)
                        sb.Append("ago");
                    return sb.ToString().Trim();

                }

                if (daysAgo < 365)
                {
                    sb.Append(getMonthsString(monthsAgo));
                    if (monthsAgo > 0)
                        sb.Append("ago");
                    return sb.ToString().Trim();

                }

                sb.Append(getYearsString(yearsAgo));
                sb.Append(getMonthsString(monthsAgoWithoutAnyYears));
                if (monthsAgoWithoutAnyYears > 1)
                    sb.Append("ago");
                return sb.ToString().Trim();

            }
        }


        private static string getSecondsString(int secAgo)
        {
            string s = "";

            if (secAgo > 0)
            {
                if (secAgo > 1)
                {
                    s = string.Format("{0:0} secs ", secAgo);
                }
                else
                {
                    s = string.Format("{0:0} sec ", secAgo);

                }
            }
            return s;
        }



        private static string getMinutesString(int minAgo)
        {
            string s = "";

            if (minAgo > 0)
            {
                if (minAgo > 1)
                {
                    s = string.Format("{0:0} mins ", minAgo);
                }
                else
                {

                }
            }
            return s;
        }

        private static string getHoursString(int hrsAgo)
        {
            string s = "";

            if (hrsAgo > 0)
            {
                if (hrsAgo > 1)
                {
                    s = string.Format("{0:0} hrs ", hrsAgo);
                }
                else
                {
                    s = string.Format("{0:0} hr ", hrsAgo);

                }
            }
            return s;
        }


        private static string getDaysString(int daysAgo)
        {
            string s = string.Format("today ");

            if (daysAgo > 0)
            {
                if (daysAgo == 1)
                {
                    s = string.Format("yesterday ");
                }
                else
                {
                    s = string.Format("{0:0} days ", daysAgo);

                }
            }

            return s;
        }



        private static string getWeekString(int weeksAgoWithoutMonths)
        {
            string s = string.Format("about a week ");

            if (weeksAgoWithoutMonths > 0)
            {
                if (weeksAgoWithoutMonths == 1)
                {
                    s = string.Format("last week ");
                }
                else
                {
                    if (weeksAgoWithoutMonths > 1)
                    {
                        s = string.Format("{0:0} weeks ", weeksAgoWithoutMonths);
                    }
                    else
                    {
                        s = string.Format("{0:0} week ", weeksAgoWithoutMonths);

                    }

                }
            }

            return s;

        }

        private static string getMonthsString(int monthsAgo)
        {
            string s = string.Format("This month ");

            if (monthsAgo > 0)
            {
                if (monthsAgo == 1)
                {
                    s = string.Format("Last month ");
                }
                else
                {
                    if (monthsAgo > 1)
                    {
                        s = string.Format("{0:0} months ", monthsAgo);
                    }
                    else
                    {
                        s = string.Format("{0:0} month ", monthsAgo);

                    }

                }
            }

            return s;
        }

        private static string getYearsString(int yrsAgo)
        {
            string s = string.Format("{0:0} years ", 0);

            if (yrsAgo > 0)
            {
                if (yrsAgo > 1)
                {
                    s = string.Format("about {0:0} yrs ", yrsAgo);
                }
                else
                {
                    s = string.Format("about {0:0} yr ", yrsAgo);

                }
            }
            return s;
        }

    }
}
