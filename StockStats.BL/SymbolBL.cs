using Alpaca.Markets;
using AutoMapper;
using StockStats.DAL;
using StockStats.Domain;
using StockStats.Domain.Entities;
using StockStats.Domain.Entities.Enums;
using StockStats.SL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockStats.BL
{
    public class SymbolBL : ISymbolBL
    {
        private readonly ISymbolRepo _symbolRepo;
        private readonly ISymbolPerformanceRepo _symbolPerformanceRepo;
        private readonly ISymbolSL _symbolSL;
        private readonly ISymbolPerformanceCollectionBL _symbolPerformanceCollectionBL;
        private readonly IMapper _mapper;


        public SymbolBL(
            ISymbolRepo symbolRepo,
            ISymbolPerformanceRepo symbolPerformanceRepo,
            ISymbolSL symbolSL,
            IMapper mapper,
            ISymbolPerformanceCollectionBL symbolPerformanceCollectionBL)
        {
            _symbolRepo = symbolRepo;
            _symbolPerformanceRepo = symbolPerformanceRepo;
            _symbolSL = symbolSL;
            _mapper = mapper;
            _symbolPerformanceCollectionBL = symbolPerformanceCollectionBL;
        }


        public async Task AddPerformanceIfNotExists(List<SymbolPerformance> symbolPerformance, UpdateFrequencyEnum updateFrequency)
        {
            if (symbolPerformance == null || symbolPerformance.Count == 0)
            {
                return;
            }

            var symbolInDb = await AddSymbolIfNotExists(symbolPerformance);

            var latestPerformance = await _symbolPerformanceRepo.GetLatestSymbolPerformance(symbolInDb.SymbolID, updateFrequency);

            var performanceToAdd = symbolPerformance.Where(x => x.PerformanceDateTime > latestPerformance.PerformanceDateTime);

            foreach (var performance in performanceToAdd)
            {
                performance.Symbol = symbolInDb;
                performance.SymbolID = symbolInDb.SymbolID;
                performance.UpdateFrequency = updateFrequency;

                await _symbolPerformanceRepo.CreateSymbolPerformance(performance);
            }
        }

        private async Task<Symbol> AddSymbolIfNotExists(List<SymbolPerformance> symbolPerformance)
        {
            var symbol = symbolPerformance.Where(x => x.Symbol != null).First();

            var symbolInDb = await _symbolRepo.GetBySymbolName(symbol.Symbol.SymbolName);

            if (symbolInDb == null)
            {
                symbolInDb = await _symbolRepo.CreateSymbol(symbol.Symbol);
            }

            return symbolInDb;
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

        public async Task<IList<SymbolPerformance>> GetSymbolPerformancesDaily(string symbol, DateRange dateRange)
        {
            var symbolPerformacesForDate = await _symbolPerformanceRepo.GetSymbolPerformance(symbol, dateRange, UpdateFrequencyEnum.Daily);

            if (_symbolPerformanceCollectionBL.IsCorrectCollectionDayRange(symbolPerformacesForDate))
            {
                return symbolPerformacesForDate;
            }
            else
            {
                var symbolPerformanceBars = await _symbolSL.GetHistory(symbol, dateRange, BarTimeFrame.Day);
                var symbolPerformance = _mapper.Map<List<SymbolPerformance>>(symbolPerformanceBars);
                await AddPerformanceIfNotExists(symbolPerformance, UpdateFrequencyEnum.Daily);
                return _mapper.Map<List<SymbolPerformance>>(symbolPerformanceBars);
            }
        }
    }
}
