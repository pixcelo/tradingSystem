using System.Text.Json;

namespace Zaku
{
    public class JsonService : IDataService
    {
        public string Path { get; set; }
        public Candle[] candles { get; set; }

        public void SetPath(string path)
        {
            this.Path = path;
        }

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
    }
}
