using System;

namespace StockStats.Domain.Entities
{
    public class SymbolPerformance : IHaveDateCreated
    {
        public int SymbolPerformanceID { get; set; }
        public int SymbolID { get; set; }
        public Symbol Symbol { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
