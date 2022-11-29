namespace Zaku
{
    public class ReportService
    {
        private decimal TotalTradingFees { get; set; }
        private decimal TotalProfit { get; set; }
        private decimal TotalLoss { get; set; }
        private int TotalNumbreOfTrading { get; set; }
        private decimal WinRate { get; set; }
        private decimal LoseRate { get; set; }
        private decimal MaxDrawdown { get; set; }

        public void AddTradingNumber(int num)
        {
            this.TotalNumbreOfTrading += num;
        }

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

        public void ComputeProfit(decimal entryPrice, decimal closePrice, OrderSide side)
        {
            decimal profit = side switch
            {
                OrderSide.Buy => closePrice - entryPrice,
                OrderSide.Sell => entryPrice - closePrice,
                _ => throw new InvalidOperationException()
            };
        }

        public void ComputeWinRate()
        {
            // 勝率
        }

        public void ComputeTotalProfit()
        {
            // 利益 - 取引手数料
        }
    }
}
