namespace Zaku
{
    public class Trade
    {
        private Logger logger { get; set; }
        private IDataService dataService { get; set; }
        private IStrategy strategy { get; set; }
        private List<Position> positions { get; set; }

        /// <summary>
        /// Trading Settings
        /// </summary>
        private readonly int lots = 1;
        private readonly int? positionLimit = null;
        private readonly int waitingTime_MilliSeconds = 1000;

        public Trade(IStrategy strategy)
        {
            this.logger = new Logger("logs/trade/");
            this.dataService = new APIService();
            this.strategy = strategy;
            this.positions = new List<Position>();
        }

        private Candle[] GetTick()
        {
            return this.dataService.GetTick();
        }

        // APIを叩く
        private void CheckPositions()
        {
            //this.dataService.CloseOrder()
        }

        /// <summary>
        /// Check number of positions
        /// </summary>
        /// <returns></returns>
        private bool WithinPositionLimit()
        {
            if (positionLimit == null)
            {
                return true;
            }

            return positionLimit >= positions.Count;
        }

        // APIを叩く
        private Position Order(Position order)
        {
            //this.dataService.PlaceOrder()
            Console.WriteLine("place order");
            return new Position();
        }

        // APIを叩く
        private Position Cancel(Position order)
        {
            //this.dataService.CancelOrder()
            Console.WriteLine("cancel order");
            return new Position(); // remove position

        }

        // APIを叩く
        public void GetPositions()
        {
            //this.dataService.GetPositions();
        }

        public void OnTick()
        {
            try
            {
                // ポジションを取得するAPI
                // this.dataService.GetPositions();

                while(true)
                {
                    Candle[] candles = GetTick();
                    var candle = candles[0];

                    CheckPositions();

                    if (!WithinPositionLimit())
                    {
                        continue;
                    }

                    // Judge entry-order-Condition
                    Condition entry = strategy.JudgeEntry(candles, 1);

                    if (!entry.IsOk)
                    {
                        Thread.Sleep(waitingTime_MilliSeconds);
                        continue;
                    }

                    var order = new Position()
                    {
                        Symbol = "BTCUSDT",
                        Type = OrderType.Market,
                        Side = entry.Side,
                        EntryPrice = candle.Close,
                        SettlementPrice = null,
                        Lots = lots,
                        TakeProfit = null,
                        StopLoss = null
                    };

                    // 注文が通ればポジションを追加する
                    var newOrder = Order(order);

                    if (newOrder != null)
                    {
                        positions.Add(newOrder);
                    }

                    Thread.Sleep(waitingTime_MilliSeconds);
                    break;
                }
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
