namespace Zaku
{
    public class Report
    {
        public decimal Profit { get; set; }
        public decimal Loss { get; set; }
        public decimal Fee { get; set; }
        public decimal RateWin { get; set; }
        public decimal RateLose { get; set; }
        public decimal MaxDrawdown { get; set; }
        public int NumberOfTrading { get; set; }
        public List<Position> List { get; set; }

        public Report()
        {
            this.List = new List<Position>();
        }
    }
}
