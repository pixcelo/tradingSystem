using System;

namespace Zaku
{
    /// <summary>
    /// crypto watch API response
    /// </summary>
    public class Candle
    {
        public long Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }

    }
}
