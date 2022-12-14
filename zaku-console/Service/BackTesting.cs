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
            this.dataService.Path = "../../jimu/candle.json";
            this.strategy = strategy;
            this.positions = new List<Position>();
            this.reportService = new ReportService();
        }

        private Candle[] GetTick()
        {
            return this.dataService.GetTick();
        }

        /// <summary>
        /// ローソク足とポジションから決済条件をチェック
        /// </summary>
        /// <param name="candle"></param>
        private void CheckPositions(Candle candle)
        {
            // 未決済ポジション
            var unsettledPositions = positions
                                     .Where(x => !x.CloseCondition)
                                     .ToList();

            foreach (var p in unsettledPositions)
            {
                if (strategy.JudgeCloseCondition(candle, p))
                {
                    p.CloseCondition = true;
                }
            }
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

        public void OnTick()
        {
            try
            {
                Candle[] candles = GetTick();

                for (int i = 0; i < candles.Length; i++)
                {
                    // ポジションをチェック
                    CheckPositions(candles[i]);
                    if (!WithinPositionLimit()) continue;

                    // エントリー条件を判定
                    Position entry = strategy.JudgeEntryCondition(candles, i);
                    if (!entry.EntryCondition) continue;

                    // 擬似オーダー
                    var randomId = new System.Random()
                                   .Next(0,10000).ToString();
                    entry.OrderId = randomId;
                    entry.Symbol = "BTCUSDT";
                    entry.Type = OrderType.Market;
                    entry.Side = entry.Side;
                    entry.EntryPrice = candles[i].Close;
                    entry.ClosePrice = null;
                    entry.Lots = lots;
                    entry.TakeProfit = null;
                    entry.StopLoss = null;
                    positions.Add(entry);
                }

                Report backTestReport = reportService.GetReport(this.positions);
                string testFileName = Path.GetFileNameWithoutExtension(this.dataService.Path);
                var es = new ExcelService(this.strategy.GetStrategyName(), testFileName);
                es.OutputAsExcelFile(backTestReport);
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
