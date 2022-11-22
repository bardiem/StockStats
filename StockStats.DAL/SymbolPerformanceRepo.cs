using Microsoft.EntityFrameworkCore;
using StockStats.Data;
using StockStats.Domain;
using StockStats.Domain.Entities;
using StockStats.Domain.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockStats.DAL
{
    public class SymbolPerformanceRepo : BaseRepo<SymbolPerformance>, ISymbolPerformanceRepo
    {
        private readonly StockStatsDbContext _context;

        public SymbolPerformanceRepo(StockStatsDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SymbolPerformance> CreateSymbolPerformance(SymbolPerformance symbolPerformance)
        {
            return await base.Create(symbolPerformance);
        }

        public async Task<SymbolPerformance> GetLatestSymbolPerformance(int symbolId, UpdateFrequencyEnum updateFrequency)
        {
            return await _context
                .SymbolPerformance
                .Where(x => x.SymbolID == symbolId)
                .OrderBy(x => x.PerformanceDateTime)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<SymbolPerformance>> GetSymbolPerformance(string symbol, DateRange dateRange, UpdateFrequencyEnum updateFrequency)
        {
            return await _context
                .SymbolPerformance
                .Where(x => x.Symbol.SymbolName == symbol && x.UpdateFrequency == updateFrequency &&
                x.PerformanceDateTime <= dateRange.RangeEnd && x.PerformanceDateTime >= dateRange.RangeStart)
                .ToListAsync();
        }
    }
}
