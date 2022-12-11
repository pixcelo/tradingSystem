namespace Zaku
{
    /// <summary>
    /// 取引結果をレポートに記録するクラス
    /// </summary>
    public class ReportService
    {
        private Report BackTestReport { get; set; }
        private decimal fee { get; set; } = 0.1M;

        public ReportService()
        {
            this.BackTestReport = new Report();
        }

        /// <summary>
        /// ポジションからバックテストの結果を集計
        /// </summary>
        /// <param name="positions"></param>
        public void Totalling(List<Position> positions)
        {
            // サマリーを計算
            foreach (var item in positions)
            {
                item.Profit = ComputeProfit(item.EntryPrice, item.ClosePrice, item.Side);
            }

            this.BackTestReport.Profit = positions.Sum(s => s.Profit);
            this.BackTestReport.Fee = positions.Sum(s => ((decimal)s.EntryPrice * s.Lots) * this.fee) +
                                      positions.Sum(s => ((decimal)s.ClosePrice * s.Lots) * this.fee);
            this.BackTestReport.MaxDrawdown = positions.Where(s => s.Profit < 0)
                                                       .Min(m => m.Profit);
            this.BackTestReport.NumberOfTrading = positions.Count();
            this.BackTestReport.RateWin = ComputeRate(positions.Where(x => x.Profit > 0).Count(), this.BackTestReport.NumberOfTrading);
            this.BackTestReport.RateLose = ComputeRate(positions.Where(x => x.Profit < 0).Count(), this.BackTestReport.NumberOfTrading);
        }

        public Report GetReport(List<Position> positions)
        {
            // 決済済ポジション
            var settledPositionsd = positions
                                    .Where(x => x.CloseCondition)
                                    .ToList();

            Totalling(settledPositionsd);
            return this.BackTestReport;
        }

        /// <summary>
        /// 売買時の利益を計算
        /// </summary>
        /// <param name="entryPrice"></param>
        /// <param name="closePrice"></param>
        /// <param name="side"></param>
        public decimal ComputeProfit(decimal entryPrice, decimal? closePrice, OrderSide side)
        {
            decimal profit = side switch
            {
                OrderSide.Buy => (decimal)closePrice - entryPrice,
                OrderSide.Sell => entryPrice - (decimal)closePrice,
                _ => throw new InvalidOperationException()
            };

            return profit;
        }

        /// <summary>
        /// 勝敗率を計算
        /// </summary>
        /// <param name="num"></param>
        /// <param name="numberOfTrading"></param>
        /// <returns></returns>
        private decimal ComputeRate(int num, int numberOfTrading)
        {
            decimal rate = (decimal)num / (decimal)numberOfTrading;
            return Math.Round(rate, 2);
        }

        private decimal ComputeTotalProfit(decimal profit, decimal fee)
        {
            return profit - fee;
        }
    }
}
