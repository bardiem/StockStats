using StockStats.Domain;
using StockStats.Domain.Entities;
using StockStats.Domain.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockStats.BL
{
    public interface ISymbolBL
    {
        CompareStocksResult GetSymbolPerformanceComparison(IList<SymbolPerformance> stock1, IList<SymbolPerformance> stock2);
        Task AddPerformanceIfNotExists(List<SymbolPerformance> symbolPerformance, UpdateFrequencyEnum updateFrequency);
    }
}
