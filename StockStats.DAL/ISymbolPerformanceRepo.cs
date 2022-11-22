using StockStats.Domain;
using StockStats.Domain.Entities;
using StockStats.Domain.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockStats.DAL
{
    public interface ISymbolPerformanceRepo
    {
        Task<SymbolPerformance> CreateSymbolPerformance(SymbolPerformance symbolPerformance);
        Task<SymbolPerformance> GetLatestSymbolPerformance(int symbolId, UpdateFrequencyEnum updateFrequency);
        Task<IList<SymbolPerformance>> GetSymbolPerformance(string symbol, DateRange dateRange, UpdateFrequencyEnum updateFrequency);
    }
}
