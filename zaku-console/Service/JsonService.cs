using System.Text.Json;

namespace Zaku
{
    public class JsonService : IDataService
    {
        public string? Path { get; set; }
        public Candle[] candles { get; set; }

        /// <summary>
        /// Deserialize Json to .NET Object
        /// </summary>
        /// <returns></returns>
        public async Task<Candle[]> GetTick()
        {
            if (this.candles != null)
            {
                return new Candle[0];
            }

            string? json = File.ReadAllText(this.Path);
            var candleStickData = JsonSerializer.Deserialize<CandleStickData>(json);
            return ConvertService.ConvertCandleSticks(candleStickData.Candlesticks);
        }

        public async Task<List<Position>> GetPositions() => new List<Position>();

        public async Task<bool> PlaceOrder(Position position) => true;
        public async Task<bool> CancelOrder(Position position) => true;
    }
}
