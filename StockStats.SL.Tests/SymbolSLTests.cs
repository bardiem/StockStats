using Xunit;
using Moq;
using Alpaca.Markets;
using StockStats.Domain;
using System.Threading;
using StockStats.SL.Tests.Helpers;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace StockStats.SL.Tests
{
    public class SymbolSLTests
    {
        private readonly SecretKey _secretKey;

        public SymbolSLTests()
        {
            _secretKey = new SecretKey("key", "api");
        }

        [Fact]
        public void GetHistory_Should_Work()
        {
            // arrange
            var symbolName = "AAPL";

            var startDate = new DateTime(2020, 11, 11);
            var endDate = new DateTime(2020, 11, 18);
            var timeFrame = BarTimeFrame.Week;

            var dateRange = new DateRange(startDate, endDate);

            var dataClient = CreateClient(symbolName);

            var sut = new SymbolSL(dataClient);

            // act
            var result = sut.GetHistory(symbolName, dateRange, timeFrame);


            // assert
            Assert.NotNull(result.Result);
            Assert.Equal(2, result.Result.Count);
            Assert.Equal(result.Result[0].Symbol, symbolName);
        }

        public IAlpacaDataClient CreateClient(string symbolName)
        {
            var clientMock = new Mock<IAlpacaDataClient>();
            var multipage = GetMultiPageItemsData(symbolName);

            clientMock
                .Setup(x => x.GetHistoricalBarsAsync(It.IsNotNull<HistoricalBarsRequest>(), It.IsAny<CancellationToken>()))
                .Returns(multipage);

            return clientMock.Object;
        }

        private Task<IMultiPage<IBar>> GetMultiPageItemsData(string symbolName)
        {
            return Task.Factory.StartNew(() => {
                MultiPage<IBar> multipage = new MultiPage<IBar>();

                IReadOnlyList<IBar> barsList = new List<Bar>()
                {
                    new Bar
                    {
                        Symbol = symbolName,
                        High = 105,
                    },
                    new Bar
                    {
                        Symbol = symbolName,
                        High = 102
                    }
                }.AsReadOnly();
                var itemsDictionary = new Dictionary<string, IReadOnlyList<IBar>>();
                itemsDictionary.Add(symbolName, barsList);

                multipage.Items = new ReadOnlyDictionary<string, IReadOnlyList<IBar>>(itemsDictionary);

                return (IMultiPage<IBar>)multipage;
            });
        }
    }
}