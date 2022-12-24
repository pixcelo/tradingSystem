namespace Zaku
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var strategy = new MovingAverageStrategy();
                var trade = new Trade(strategy);
                //var trade = new BackTesting(strategy);
                await trade.OnTick();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message +
                    Environment.NewLine +
                    ex.StackTrace);
            }
        }
    }
}
