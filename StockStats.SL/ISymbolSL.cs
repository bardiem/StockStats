using Alpaca.Markets;
using StockStats.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockStats.SL
{
    public interface ISymbolSL
    {
        Task<IReadOnlyList<IBar>> GetHistory(string symbolName, DateRange dateRange, BarTimeFrame timeframe);
    }
}
