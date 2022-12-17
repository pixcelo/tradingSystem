using Binance.Net.Clients;

namespace Zaku
{
    public class APIService : IDataService
    {
        public string Path { get; set; }
        public Candle[] candles { get; set; }

        private BinanceService binance { get; set; }

        public APIService(BinanceService binance)
        {
            this.binance = binance;
        }

        public async Task<Candle[]> GetTickAsync()
        {
            var spotTradeHistory = await this.binance.GetTick();
            var candles = new List<Candle>();

            foreach (var item in spotTradeHistory)
            {
                DateTimeOffset utcTime = new DateTimeOffset(item.TradeTime, TimeSpan.Zero);
                long unixTime = utcTime.ToUnixTimeSeconds();

                var c = new Candle();
                c.Date = unixTime;
                c.Close = item.Price;
                candles.Add(c);
            }

            return candles.ToArray();
        }

        public List<Position> GetPositions()
        {
            return new List<Position>();
        }
    }
}
