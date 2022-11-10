using StockStats.Data;

namespace StockStats.DAL
{
    public class SymbolPerformanceRepo : ISymbolPerformanceRepo
    {
        private readonly StockStatsDbContext _context;

        public SymbolPerformanceRepo(StockStatsDbContext context)
        {
            _context = context;
        }
    }
}
