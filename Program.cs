using System.Text.Json;

namespace Zaku
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../jimu/candle.json";
            string? json = File.ReadAllText(filePath);
            var candleSticks = JsonSerializer.Deserialize<CandleStickData>(json);
            Console.WriteLine(candleSticks);
        }
    }
}
