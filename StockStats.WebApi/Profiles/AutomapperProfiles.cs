using Alpaca.Markets;
using AutoMapper;
using StockStats.Domain.Entities;
using StockStats.WebApi.Dtos;

namespace StockStats.WebApi.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<SymbolPerformanceReadDto, SymbolPerformance>()
                .ForPath(d => d.Symbol.SymbolName, opt => opt.MapFrom(s => s.SymbolName))
                .ReverseMap()
                .ForPath(d => d.SymbolName, opt => opt.MapFrom(x => x.Symbol.SymbolName));

            CreateMap<IBar, SymbolPerformance>()
                .ForPath(d => d.Symbol.SymbolName, opt => opt.MapFrom(s => s.Symbol))
                .ForMember(d => d.PerformanceDateTime, opt => opt.MapFrom(s => s.TimeUtc))
                .ForMember(d => d.AveragePrice, opt => opt.MapFrom(s => (s.Low + s.High) / 2));
        }
    }
}
