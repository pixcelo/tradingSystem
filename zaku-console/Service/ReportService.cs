namespace Zaku
{
    public class ReportService
    {
        private decimal TotalTradingFees { get; set; }
        private decimal TotalProfit { get; set; }
        private decimal TotalLoss { get; set; }
        private int NumberOfTrading { get; set; }
        private int NumberOfWins {get; set;}
        private int NumberOfLosses {get; set;}
        private decimal MaxDrawdown { get; set; }

        /// <summary>
        /// 取引手数料は売買時に発生
        /// </summary>
        /// <param name="orderPrice"></param>
        /// <param name="feeRate"></param>
        public void AddTradingFee(decimal orderPrice, decimal feeRate)
        {
            decimal tradingFee = orderPrice * feeRate;
            this.TotalTradingFees += tradingFee;
        }

        /// <summary>
        /// 売買時の利益を計算
        /// </summary>
        /// <param name="entryPrice"></param>
        /// <param name="closePrice"></param>
        /// <param name="side"></param>
        public void ComputeProfit(decimal entryPrice, decimal closePrice, OrderSide side)
        {
            decimal profit = side switch
            {
                OrderSide.Buy => closePrice - entryPrice,
                OrderSide.Sell => entryPrice - closePrice,
                _ => throw new InvalidOperationException()
            };

            UpdateStatus(profit);
        }

        /// <summary>
        /// ステータスを更新
        /// </summary>
        /// <param name="profit"></param>
        private void UpdateStatus(decimal profit)
        {
            this.TotalProfit += profit;
            this.NumberOfTrading++;

            if (profit > 0)
            {
                this.NumberOfWins++;
            }
            else
            {
                this.NumberOfLosses++;
            }
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
