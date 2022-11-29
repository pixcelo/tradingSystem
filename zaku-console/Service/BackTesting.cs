namespace Zaku
{
    public class BackTesting
    {
        private Logger logger { get; set; }
        private IDataService dataService { get; set; }
        private IStrategy strategy { get; set; }
        private List<Position> positions { get; set; }
        private ReportService reportService { get; set;}

        /// <summary>
        /// BackTest Settings
        /// </summary>
        private readonly int lots = 1;
        private readonly int? positionLimit = null;
        private readonly int Slippage = 0;
        private readonly decimal feeRate = 0.1M;

        public BackTesting(IStrategy strategy)
        {
            this.logger = new Logger("logs/backTesting/");
            this.dataService = new JsonService();
            this.dataService.Path = "../jimu/candle.json";
            this.strategy = strategy;
            this.positions = new List<Position>();
            this.reportService = new ReportService();
        }

        private Candle[] GetTick()
        {
            return this.dataService.GetTick();
        }

        private void CheckPositions(Candle candle)
        {
            if (positions.Count > 0)
            {
                foreach (var p in positions)
                {
                    // Judge close-position-condition
                    Condition close = strategy.JudgeClose(candle, p);

                    if (close.IsOk)
                    {
                        ClosePosition(close);
                    }
                }
            }
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

        private void ClosePosition(Condition condition)
        {
            var position = positions.Where(x => x.OrderId == condition.OrderId).FirstOrDefault();

            if (position == null)
            {
                return;
            }

            reportService.AddTradingFee(condition.ClosePrice, feeRate);
            reportService.ComputeProfit(condition.EntryPrice, condition.ClosePrice, condition.Side);
            positions.RemoveAll(x => x.OrderId == position.OrderId);
        }

        public void OnTick()
        {
            try
            {
                Candle[] candles = GetTick();

                for (int i = 0; i < candles.Length; i++)
                {
                    CheckPositions(candles[i]);

                    if (!WithinPositionLimit())
                    {
                        continue;
                    }

                    // Judge entry-order-Condition
                    Condition entry = strategy.JudgeEntry(candles, i);

                    if (!entry.IsOk)
                    {
                        continue;
                    }

                    var randomId = new System.Random()
                        .Next(0,10000).ToString();

                    var order = new Position()
                    {
                        OrderId = randomId,
                        Symbol = "BTCUSDT",
                        Type = OrderType.Market,
                        Side = entry.Side,
                        EntryPrice = candles[i].Close,
                        SettlementPrice = null,
                        Lots = lots,
                        TakeProfit = null,
                        StopLoss = null
                    };

                    reportService.AddTradingFee(order.EntryPrice, feeRate);
                    positions.Add(order);
                }
            }
            catch (Exception ex)
            {
                logger.logs.Add(ex.Message +
                    Environment.NewLine +
                    ex.StackTrace);
            }
            finally
            {
                logger.WriteLog();
            }
        }

    }
}
