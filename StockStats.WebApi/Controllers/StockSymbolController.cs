using Microsoft.AspNetCore.Mvc;
using StockStats.BL;
using StockStats.SL;

namespace StockStats.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockSymbolController : ControllerBase
    {
        private readonly ISymbolBL _symbolBL;
        private readonly ISymbolSL _symbolSL;
        public StockSymbolController(ISymbolBL symbolBL, ISymbolSL symbolSL)
        {
            _symbolBL = symbolBL;
            _symbolSL = symbolSL;
        }

        [HttpGet]
        [Route("{symbolName}")]
        public async Task<IActionResult> GetSymbolWeekPerfomance(string symbolName)
        {
            var bars = await _symbolSL.GetHistory(symbolName, DateTime.UtcNow.AddDays(-7), DateTime.UtcNow.AddMinutes(-16));
            if (bars == null || bars.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(bars);
        }
    }
}
