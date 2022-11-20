namespace Zaku
{
    public static class ConvertService
    {
        public static Candle[] ConvertCandleSticks(List<List<decimal>>? candleStickData)
        {
            if (candleStickData == null || candleStickData.Count == 0)
            {
                return new Candle[0];
            }

            var arry = new Candle[candleStickData.Count];

            foreach (var candles in candleStickData)
            {
                var c = new Candle();
                c.Date = candles[0];
                c.Open = candles[1];
                c.High = candles[2];
                c.Low = candles[3];
                c.Close = candles[4];
                c.Volume = candles[5];

                arry.Append(c);
            }

            return arry;
        }
    }
}
