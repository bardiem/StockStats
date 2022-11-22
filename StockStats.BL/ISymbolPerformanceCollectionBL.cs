using StockStats.Domain.Entities;
using System.Collections.Generic;

namespace StockStats.BL
{
    public interface ISymbolPerformanceCollectionBL
    {
        bool IsCorrectCollectionDayRange(IList<SymbolPerformance> symbolPerformanceRange);
    }
}
