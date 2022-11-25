namespace Zaku
{
    public class BackTesting
    {
        private IDataService dataService { get; set; }
        private IStrategy strategy { get; set; }

        /// <summary>
        /// BackTest Settings
        /// </summary>
        private readonly int lots = 1;
        private readonly int positionLimit = 0;
        private readonly int Slippage = 0;
        private readonly decimal commission = 0.1M;

        public BackTesting(IStrategy strategy)
        {
            this.dataService = new JsonService();
            this.strategy = strategy;
            this.dataService.Path = "../jimu/candle.json";
        }

        public Candle[] GetTick()
        {
            return this.dataService.GetTick();
        }

        // 擬似トレード
        public Position Order(Order order)
        {
            // TODO: 計算をする処理
            Console.WriteLine("result: ");
            return new Position();
        }

        public void OnTick()
        {
            var logger = new Logger("logs/");
            var log = new List<string>();

            var positions = new List<Position>();

            try
            {
                Candle[] candles = GetTick();

                foreach (var candle in candles)
                {
                    if (positions.Count > 0)
                    {
                        // 決済条件を判定
                        var conditions = strategy.JudgePositions(positions);

                        foreach (var cd in conditions)
                        {
                            if (cd.IsOk)
                            {
                                // TODO: 条件を満たしている場合、決済オーダーを入れる
                                // ClosePosition(cd.OrderId);
                            }
                        }
                    }

                    var condition = strategy.Judge(candle);
                    if (!condition.IsOk)
                    {
                        continue;
                    }

                    var order = new Order()
                    {
                        Symbol = "BTCUSDT",
                        OrderSide = condition.Side,
                        EntryPrice = candle.Close,
                        SettlementPrice = null,
                        Lots = lots,
                        TakeProfit = null,
                        StopLoss = null
                    };

                    var position = Order(order);
                    positions.Add(position);
                }
            }
            catch (Exception ex)
            {
                log.Add(ex.Message +
                    Environment.NewLine +
                    ex.StackTrace);
            }
            finally
            {
                logger.WriteLog(log);
            }
        }

    }
}
