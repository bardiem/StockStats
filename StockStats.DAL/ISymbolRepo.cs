using StockStats.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockStats.DAL
{
    public interface ISymbolRepo
    {
        Task<Symbol> CreateSymbol(Symbol symbol);

        Task<List<Symbol>> Get();

        Task<Symbol> GetBySymbolName(string symbolName);
    }
}
