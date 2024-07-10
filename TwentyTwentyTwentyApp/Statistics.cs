using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TwentyTwentyTwentyApp
{
    public class Statistics
    {
        private const string StatisticsFilePath = "statistics.json";
        public Dictionary<string, int> DailyBreaks { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> WeeklyBreaks { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> MonthlyBreaks { get; set; } = new Dictionary<string, int>();

        public static Statistics LoadStatistics()
        {
            if (File.Exists(StatisticsFilePath))
            {
                var statisticsJson = File.ReadAllText(StatisticsFilePath);
                return JsonConvert.DeserializeObject<Statistics>(statisticsJson);
            }
            return new Statistics();
        }

        public void SaveStatistics()
        {
            var statisticsJson = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(StatisticsFilePath, statisticsJson);
        }

        public void AddBreak()
        {
            var today = DateTime.Today.ToString("yyyy-MM-dd");
            var week = $"{DateTime.Now.Year}-W{System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday)}";
            var month = DateTime.Now.ToString("yyyy-MM");

            if (DailyBreaks.ContainsKey(today))
            {
                DailyBreaks[today]++;
            }
            else
            {
                DailyBreaks[today] = 1;
            }

            if (WeeklyBreaks.ContainsKey(week))
            {
                WeeklyBreaks[week]++;
            }
            else
            {
                WeeklyBreaks[week] = 1;
            }

            if (MonthlyBreaks.ContainsKey(month))
            {
                MonthlyBreaks[month]++;
            }
            else
            {
                MonthlyBreaks[month] = 1;
            }

            SaveStatistics();
        }
    }
}
