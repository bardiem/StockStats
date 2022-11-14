namespace StockStats.WebApi.Dtos
{
    public class SymbolPerformanceReadDto
    {
        public string SymbolName { get; set; }
        public DateTime PerformanceDateTime { get; set; }
        public decimal AveragePrice { get; set; }
    }
}
