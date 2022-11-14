using Alpaca.Markets;
using System.Collections.Generic;

namespace StockStats.SL.Tests.Helpers
{
    internal class MultiPage<TItems> : IMultiPage<TItems>
    {
        public string? NextPageToken { get; set; }

        public IReadOnlyDictionary<string, IReadOnlyList<TItems>> Items { get; set; }
    }
}
