using Alpaca.Markets;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockStats.BL;
using StockStats.DAL;
using StockStats.Domain;
using StockStats.Domain.Entities;
using StockStats.Domain.Entities.Enums;
using StockStats.SL;
using StockStats.WebApi.Dtos;

namespace StockStats.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockSymbolController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISymbolBL _symbolBL;
        private readonly ISymbolSL _symbolSL;
        private readonly ISymbolRepo _symbolRepo;

        public StockSymbolController(ISymbolBL symbolBL, ISymbolSL symbolSL, IMapper mapper, ISymbolRepo symbolRepo)
        {
            _symbolBL = symbolBL;
            _symbolSL = symbolSL;
            _mapper = mapper;
            _symbolRepo = symbolRepo;
        }

        [HttpGet]
        [Route("StockPerformanceWithSnPForWeek/{symbolName}")]
        public async Task<IActionResult> GetStockPerformanceWithSnPForWeek(string symbolName)
        {
            var lastWeekDateRange = DateRange.GetLastWeekDateRange();
            var symbolBars = await _symbolSL.GetHistory(symbolName, lastWeekDateRange, BarTimeFrame.Day);
            if (symbolBars == null || symbolBars.Count == 0)
            {
                return NotFound();
            }
            var snpBars = await _symbolSL.GetHistory("SPY", lastWeekDateRange, BarTimeFrame.Day);

            var symbolPerfBars = _mapper.Map<List<SymbolPerformance>>(symbolBars);
            var snpPerfBars = _mapper.Map<List<SymbolPerformance>>(snpBars);

            await _symbolBL.AddPerformanceIfNotExists(symbolPerfBars, UpdateFrequencyEnum.Daily);
            await _symbolBL.AddPerformanceIfNotExists(snpPerfBars, UpdateFrequencyEnum.Daily);

            var result = _symbolBL.GetSymbolPerformanceComparison(symbolPerfBars, snpPerfBars);

            return Ok(result);
        }

        [HttpGet]
        [Route("StockPerformanceWithSnPForWeekFromDB/{symbolName}")]
        public async Task<IActionResult> GetStockPerformanceWithSnPForWeekFromDB(string symbolName)
        {
            var lastWeekDateRange = DateRange.GetLastWeekDateRange();
            var symbolPerfBars = await _symbolBL.GetSymbolPerformancesDaily(symbolName, lastWeekDateRange);
            if (symbolPerfBars == null || symbolPerfBars.Count == 0)
            {
                return NotFound();
            }
            var snpPerfBars = await _symbolBL.GetSymbolPerformancesDaily("SPY", lastWeekDateRange);
            var result = _symbolBL.GetSymbolPerformanceComparison(symbolPerfBars, snpPerfBars);

            return Ok(result);
        }

        [HttpGet]
        [Route("StockPerformanceWithSnPForDay/{symbolName}")]
        public async Task<IActionResult> GetStockPerformanceWithSnPForDay(string symbolName)
        {
            var dayDateRangeForAlpaca = new DateRange(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddMinutes(-15).AddSeconds(-2));
            var symbolBars = await _symbolSL.GetHistory(symbolName, dayDateRangeForAlpaca, BarTimeFrame.Hour);
            if (symbolBars == null || symbolBars.Count == 0)
            {
                return NotFound();
            }
            var snpBars = await _symbolSL.GetHistory("SPY", dayDateRangeForAlpaca, BarTimeFrame.Hour);

            var symbolPerfBars = _mapper.Map<List<SymbolPerformance>>(symbolBars);
            var snpPerfBars = _mapper.Map<List<SymbolPerformance>>(snpBars);

            await _symbolBL.AddPerformanceIfNotExists(symbolPerfBars, UpdateFrequencyEnum.Hourly);
            await _symbolBL.AddPerformanceIfNotExists(snpPerfBars, UpdateFrequencyEnum.Hourly);

            var result = _symbolBL.GetSymbolPerformanceComparison(symbolPerfBars, snpPerfBars);

            return Ok(result);
        }
    }
}
