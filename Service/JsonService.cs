using System.Text.Json;

namespace Zaku
{
    public class JsonService : IDataService
    {
        public string path { get; set; }

        public void SetPath(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Deserialize Json to .NET Object
        /// </summary>
        /// <returns></returns>
        public void GetTick()
        {
            string? json = File.ReadAllText(this.path);
            var candleSticks = JsonSerializer.Deserialize<CandleStickData>(json);
            Console.WriteLine(candleSticks);
        }
    }
}
