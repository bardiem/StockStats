using StockStats.DAL;
using StockStats.Domain;
using StockStats.Domain.Entities;
using StockStats.Domain.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockStats.BL
{
    public class SymbolBL : ISymbolBL
    {
        private readonly ISymbolRepo _symbolRepo;
        private readonly ISymbolPerformanceRepo _symbolPerformanceRepo;


        public SymbolBL(ISymbolRepo symbolRepo, ISymbolPerformanceRepo symbolPerformanceRepo)
        {
            _symbolRepo = symbolRepo;
            _symbolPerformanceRepo = symbolPerformanceRepo;
        }


        public async Task AddPerformanceIfNotExists(List<SymbolPerformance> symbolPerformance, UpdateFrequencyEnum updateFrequency)
        {
            foreach (var performance in symbolPerformance)
            {
                var symbolInDb = await _symbolRepo.GetBySymbolName(performance.Symbol.SymbolName);

                if (symbolInDb == null)
                {
                    symbolInDb = await _symbolRepo.CreateSymbol(performance.Symbol);
                }

                var latestPerformance = await _symbolPerformanceRepo.GetLatestSymbolPerformance(symbolInDb.SymbolID, updateFrequency);

                if (latestPerformance == null || latestPerformance?.PerformanceDateTime < performance.PerformanceDateTime)
                {
                    performance.Symbol = symbolInDb;
                    performance.SymbolID = symbolInDb.SymbolID;

                    await _symbolPerformanceRepo.CreateSymbolPerformance(performance);
                }
            }
        }

        public CompareStocksResult GetSymbolPerformanceComparison(IList<SymbolPerformance> stock1, IList<SymbolPerformance> stock2)
        {
            var comparisonResult = new CompareStocksResult();

            if (stock1 == null || stock1.Count == 0 || stock2 == null || stock2.Count == 0 || stock1.Count != stock2.Count)
            {
                return comparisonResult;
            }

            comparisonResult.Stock1Name = stock1[0]?.Symbol.SymbolName;
            comparisonResult.Stock2Name = stock2[0]?.Symbol.SymbolName;

            for (var i = 0; i < stock1.Count; i++)
            {
                comparisonResult.PerformanceDates.Add(stock1[i].PerformanceDateTime);

                comparisonResult.Stock1Average.Add(stock1[i].AveragePrice);
                comparisonResult.Stock2Average.Add(stock2[i].AveragePrice);

                if (i == 0)
                {
                    comparisonResult.Stock1Performance.Add(0);
                    comparisonResult.Stock2Performance.Add(0);
                }
                else
                {
                    var stock1Increase = (float)((stock1[i].AveragePrice - stock1[0].AveragePrice) / stock1[0].AveragePrice * 100);
                    comparisonResult.Stock1Performance.Add(stock1Increase);

                    var stock2Increase = (float)((stock2[i].AveragePrice - stock2[0].AveragePrice) / stock2[0].AveragePrice * 100);
                    comparisonResult.Stock2Performance.Add(stock2Increase);
                }
            }
            return comparisonResult;
        }
    }
}
