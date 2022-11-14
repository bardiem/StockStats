using System;

namespace StockStats.Domain
{
    public class DateRange
    {
        public DateRange(DateTime rangeStart, DateTime rangeEnd)
        {
            RangeStart = rangeStart;
            RangeEnd = rangeEnd;
        }

        public DateTime RangeStart { get; private set; }
        public DateTime RangeEnd { get; private set; }

        public bool IsDateInRange(DateTime date)
        {
            return date <= RangeEnd && date >= RangeStart;
        }

        public static DateRange GetLastWeekDateRange()
        {
            var mondayOfTheLastWeek = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek - 6);
            var firstTimeOfLastWeek = new DateTime(mondayOfTheLastWeek.Year, mondayOfTheLastWeek.Month, mondayOfTheLastWeek.Day, 0, 0, 1);
            var lastMinuteOfLastWeek = firstTimeOfLastWeek.AddDays(7);
            var weekDateRangeForAlpaca = new DateRange(firstTimeOfLastWeek, lastMinuteOfLastWeek);
            return weekDateRangeForAlpaca;
        }
    }
}
