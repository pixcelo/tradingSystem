namespace Zaku
{
    public enum Side
    {
        Buy,
        Sell
    }

    public class Order
    {
        public Side OrderSide { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal SettlementPrice { get; set; }
        public decimal Profit { get; set; }
        public decimal Loss { get; set; }
    }
}
