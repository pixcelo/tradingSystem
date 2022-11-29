using System.Text.Json;

namespace Zaku
{
    public class APIService : IDataService
    {
        public string Path { get; set; }
        public Candle[] candles { get; set; }

        public Candle[] GetTick()
        {
            Console.WriteLine("This is the API Service");
            return new Candle[0];
        }

        public List<Position> GetPositions()
        {
            return new List<Position>();
        }
    }
}
