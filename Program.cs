namespace Zaku
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../jimu/candle.json";
            var trade = new Trade(new JsonService());
            trade.SetPath(filePath);
            trade.GetTick();
        }
    }
}
