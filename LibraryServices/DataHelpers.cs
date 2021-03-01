using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryServices
{
    class DataHelpers
    {
        public static List<string> HumanBizHours(IEnumerable<BranchHours> branchHours)
        {
            var hours = new List<string>();
            foreach(var time in branchHours)
            {
                var day = HumanizeDay(time.DayOfWeek);
                var OpenTime = HumanizeTime(time.OpenTime);
                var closeTime = HumanizeTime(time.CloseTime);

                var timeEntry = $"{day} {OpenTime} to {closeTime}";
                hours.Add(timeEntry);
            }

            return hours;
        }

        public static string HumanizeDay(int number)
        {
            return Enum.GetName(typeof(DayOfWeek), number);
        }

        public static string HumanizeTime(int time)
        {
            return TimeSpan.FromHours(time).ToString("hh':'mm");
        }
    }
}
