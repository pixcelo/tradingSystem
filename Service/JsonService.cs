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
        public void GetTick()
        {
            if (this.candles != null)
            {
                return;
            }

            string? json = File.ReadAllText(this.Path);
            var candleStickData = JsonSerializer.Deserialize<CandleStickData>(json);
            this.candles = ConvertService.ConvertCandleSticks(candleStickData.Candlesticks);
        }
    }
}
