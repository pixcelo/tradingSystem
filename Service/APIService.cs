using System.Text.Json;

namespace Zaku
{
    public class APIService : IDataService
    {
        public string Path { get; set; }
        public Candle[] candles { get; set; }

        public void GetTick()
        {
            this.candles = new Candle[0];
            Console.WriteLine("This is the API Service");
        }
    }
}
