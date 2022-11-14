﻿using Alpaca.Markets;
using StockStats.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StockStats.SL
{
    public class SymbolSL : ISymbolSL
    {
        private readonly IEnvironment _env;
        private readonly SecretKey _secretKey;

        public SymbolSL(SecretKey secretKey, IEnvironment env)
        {
            _secretKey = secretKey;
            _env = env;
        }

        public async Task<IReadOnlyList<IBar>> GetHistory(string symbolName, DateRange dateRange, BarTimeFrame timeframe)
        {
            var client = _env.GetAlpacaDataClient(_secretKey);
            var barsDictionary = await client.GetHistoricalBarsAsync(new HistoricalBarsRequest(symbolName, dateRange.RangeStart, dateRange.RangeEnd, timeframe));
            return barsDictionary.Items.FirstOrDefault().Value;
        }
    }
}
