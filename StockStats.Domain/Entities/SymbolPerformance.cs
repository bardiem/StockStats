using StockStats.Domain.Entities.Enums;
using System;

namespace StockStats.Domain.Entities
{
    public class SymbolPerformance : IHaveDateCreated
    {
        public int SymbolPerformanceID { get; set; }
        public int SymbolID { get; set; }
        public Symbol Symbol { get; set; }
        public DateTime PerformanceDateTime { get; set; }
        public decimal AveragePrice { get; set; }
        public UpdateFrequencyEnum UpdateFrequency { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
