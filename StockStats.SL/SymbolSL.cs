using Alpaca.Markets;
using StockStats.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StockStats.SL
{
    public class SymbolSL : ISymbolSL
    {
        private readonly IAlpacaDataClient _dataClient;

        public SymbolSL(IAlpacaDataClient dataClient)
        {
            _dataClient = dataClient;
        }

        public async Task<IReadOnlyList<IBar>> GetHistory(string symbolName, DateRange dateRange, BarTimeFrame timeframe)
        {
            var barsDictionary = await _dataClient.GetHistoricalBarsAsync(new HistoricalBarsRequest(symbolName, dateRange.RangeStart, dateRange.RangeEnd, timeframe));
            return barsDictionary.Items.FirstOrDefault().Value;
        }
    }
}
