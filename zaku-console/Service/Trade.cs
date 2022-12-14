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
        private readonly string symbol = "BTCUSDT";
        private readonly int lots = 1;
        private readonly int? positionLimit = null;
        private readonly int waitingTime_MilliSeconds = 1000;

        public Trade(IStrategy strategy)
        {
            this.logger = new Logger("logs/trade/");
            //this.dataService = new ExchangeService(new HttpClient());
            this.dataService = new BinanceService();
            this.strategy = strategy;
            this.positions = new List<Position>();
        }

        private async Task<Candle[]> GetTick()
        {
            return await this.dataService.GetTick();
        }

        /// <summary>
        /// 資産残高を取得
        /// </summary>
        /// <returns></returns>
        private async Task GetBalance()
        {
            await this.dataService.GetBalance();
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

        /// <summary>
        /// 注文を出す
        /// </summary>
        /// <param name="order"></param>
        private void Order(Position order)
        {
            order.Symbol = this.symbol;
            order.Lots = this.lots;
            this.dataService.PlaceOrder(order);
        }

        /// <summary>
        /// 注文をキャンセル
        /// </summary>
        /// <param name="orderId"></param>
        private async Task Cancel(string orderId)
        {
            bool isSuccess = await this.dataService.CancelOrder(orderId);
        }

        /// <summary>
        /// 現在のポジションを取得
        /// </summary>
        public async void GetPositions()
        {
            this.positions = await this.dataService.GetPositions();
        }

        public async Task OnTick()
        {
            try
            {
                await GetBalance();

                while(true)
                {
                    Candle[] candles = await GetTick();
                    Thread.Sleep(waitingTime_MilliSeconds);

                    GetPositions();
                    if (!WithinPositionLimit()) continue;

                    // エントリー条件を判定
                    Position entry = strategy.JudgeEntryCondition(candles, 1);
                    if (!entry.EntryCondition) continue;

                    // 売買オーダー
                    Order(entry);

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
