
namespace Zaku
{
    public class Position
    {
        public string? OrderId { get; set; }
        public string? Symbol { get; set; }
        public OrderType Type { get; set; }
        public OrderSide Side { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal Lots { get; set; }
        public decimal? TakeProfit { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal Profit { get; set; }
        public decimal Fee { get; set; }
        public bool EntryCondition { get; set; }
        public bool CloseCondition { get; set; }

        public Position()
        {
            this.EntryCondition = false;
            this.CloseCondition = false;
        }
    }
}
