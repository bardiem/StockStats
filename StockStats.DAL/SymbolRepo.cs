using Microsoft.EntityFrameworkCore;
using StockStats.Data;
using StockStats.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockStats.DAL
{
    public class SymbolRepo : BaseRepo<Symbol>, ISymbolRepo
    {
        private readonly StockStatsDbContext _context;

        public SymbolRepo(StockStatsDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Symbol> CreateSymbol(Symbol symbol)
        {
            return await base.Create(symbol);
        }

        public async Task<List<Symbol>> Get()
        {
            return await _context.Symbols.ToListAsync();
        }

        public async Task<Symbol> GetBySymbolName(string symbolName)
        {
            return await _context.Symbols.FirstOrDefaultAsync(x => x.SymbolName == symbolName);
        }
    }
}
