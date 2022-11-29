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

        public void AddTradingFee(decimal tradingFee)
        {
            this.TotalTradingFees += tradingFee;
        }

        public void ComputeWinRate()
        {
            // 勝率
        }
    }
}
