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
    }
}
