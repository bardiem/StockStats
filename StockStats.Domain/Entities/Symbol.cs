using System;

namespace StockStats.Domain.Entities
{
    public class Symbol : IHaveDateCreated
    {
        public int SymbolID { get; set; }
        public string SymbolName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
