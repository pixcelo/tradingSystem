namespace Zaku
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var strategy = new MovingAverageStrategy();
                //var trade = new Trade(strategy);
                var trade = new BackTesting(strategy);
                trade.OnTick();
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
