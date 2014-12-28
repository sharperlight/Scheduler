using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIELibrary
{
    public class DateLibrary
    {
        public DateTime currentFortNightStartDate = new DateTime();
        public DateTime currentFortNightEndDate = new DateTime();
        public DateTime[] wholeFortNightDate = new DateTime[26];



        public DateLibrary()
        {
            CalculateFortNightYear();
        }


        private void CalculateFortNightYear()
        {
            DateTime todayDate = DateTime.Today;
            //calc fornights
            DateTime startDate = new DateTime(todayDate.Year, 1, 1);
            int count = 0;
            while (startDate.DayOfWeek.ToString().CompareTo("Monday") != 0 && count < 7)
            {
                startDate = startDate.AddDays(1);
                count++;
            }
            wholeFortNightDate[0] = startDate;
            DateTime fortNightDate = startDate;
            bool flag = false;
            if (todayDate.CompareTo(startDate) < 0)
            {
                currentFortNightStartDate = startDate;
                flag = true;
            }
            //MessageBox.Show(wholeFornightDate[0].ToString() + " " + wholeFornightDate[0].DayOfWeek.ToString());
            for (int i = 1; i < 26; i++)
            {
                if (todayDate.CompareTo(fortNightDate) < 0 && !flag)
                {
                    currentFortNightStartDate = wholeFortNightDate[i - 2];
                    flag = true;
                }
                fortNightDate = fortNightDate.AddDays(14);
                wholeFortNightDate[i] = fortNightDate;
                //MessageBox.Show(wholeFornightDate[i].ToString() + " " + wholeFornightDate[i].DayOfWeek.ToString());
            }
            currentFortNightEndDate = currentFortNightStartDate.AddDays(13);
        }

        public int GetDayPosition(string day)
        {
            switch (day)
            {
                case "Sunday":
                    return 0;
                case "Monday":
                    return 1;
                case "Tuesday":
                    return 2;
                case "Wednesday":
                    return 3;
                case "Thursday":
                    return 4;
                case "Friday":
                    return 5;
                case "Saturday":
                    return 6;
                default:
                    return 0;
            }
        }

        public string DateTimeConvertSQL(DateTime d)
        {
            return d.Date.ToString("yyyy-MM-dd");
        }

        public string DateTimeConvertString(DateTime d)
        {
            return d.Date.ToString();
        }

        public class CalendarStore
        {

            public LinkedList<DateTime> store;

            public CalendarStore()
            {
                store = new LinkedList<DateTime>();
            }

        }
    }
}
