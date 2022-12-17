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
            this.dataService = new APIService(new BinanceService());
            this.strategy = strategy;
            this.positions = new List<Position>();
        }

        private async Task<Candle[]> GetTick()
        {
            return await this.dataService.GetTick();
        }

        // APIを叩く
        private void CheckPositions()
        {
            //this.dataService.CloseOrder()
        }

        /// <summary>
        /// 保持するポジション数の範囲内かをチェック
        /// </summary>
        /// <returns></returns>
        private bool WithinPositionLimit()
        {
            return positionLimit == null
                   ? true
                   : positionLimit >= positions.Count;
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

        public async void OnTick()
        {
            try
            {
                // ポジションを取得するAPI
                // this.dataService.GetPositions();

                while(true)
                {
                    Candle[] candles = await GetTick();
                    var candle = candles[0];

                    // ポジションをチェック
                    CheckPositions();
                    if (!WithinPositionLimit()) continue;

                    // エントリー条件を判定
                    Position entry = strategy.JudgeEntryCondition(candles, 1);
                    if (!entry.EntryCondition)
                    {
                        Thread.Sleep(waitingTime_MilliSeconds);
                        continue;
                    }

                    entry.Symbol = "BTCUSDT";
                    entry.Type = OrderType.Market;
                    entry.Side = entry.Side;
                    entry.EntryPrice = candle.Close;
                    entry.ClosePrice = null;
                    entry.Lots = lots;
                    entry.TakeProfit = null;
                    entry.StopLoss = null;

                    // 注文が通ればポジションを追加する
                    var newOrder = Order(entry);

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
