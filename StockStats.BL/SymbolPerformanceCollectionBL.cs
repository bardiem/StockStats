using StockStats.Domain.Entities;
using System;
using System.Collections.Generic;

namespace StockStats.BL
{
    public class SymbolPerformanceCollectionBL : ISymbolPerformanceCollectionBL
    {
        public bool IsCorrectCollectionDayRange(IList<SymbolPerformance> symbolPerformanceRange)
        {
            for (var i = 1; i < symbolPerformanceRange.Count; i++)
            {
                if (!IsOneDayHasPast(symbolPerformanceRange, i) || !IsStartOfWeek(symbolPerformanceRange, i))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsStartOfWeek(IList<SymbolPerformance> symbolPerformanceRange, int i)
        {
            return (symbolPerformanceRange[i].PerformanceDateTime.DayOfWeek == DayOfWeek.Monday && (symbolPerformanceRange[i].PerformanceDateTime - symbolPerformanceRange[i - 1].PerformanceDateTime).Days == 3);
        }

        private static bool IsOneDayHasPast(IList<SymbolPerformance> symbolPerformanceRange, int i)
        {
            return (symbolPerformanceRange[i].PerformanceDateTime - symbolPerformanceRange[i - 1].PerformanceDateTime).Days == 1;
        }
    }
}
