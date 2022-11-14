using System;
using System.Collections.Generic;
using System.Text;

namespace StockStats.Domain
{
    public class CompareStocksResult
    {
        public string Stock1Name { get; set; }
        public string Stock2Name { get; set; }
        public IList<DateTime> PerformanceDates { get; set; } = new List<DateTime>();
        public IList<decimal> Stock1Average { get; set; } = new List<decimal>();
        public IList<decimal> Stock2Average { get; set; } = new List<decimal>();
        public IList<float> Stock1Performance { get; set; } = new List<float>();
        public IList<float> Stock2Performance { get; set; } = new List<float>();
    }
}
