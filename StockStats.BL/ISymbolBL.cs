using StockStats.Domain;
using StockStats.Domain.Entities;

namespace StockStats.BL
{
    public interface ISymbolBL
    {
        Symbol GetSymbolByName(string symbolName);
    }
}
