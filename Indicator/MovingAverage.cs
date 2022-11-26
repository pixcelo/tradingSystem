namespace Zaku
{
    public static class MovingAverage
    {
        private static Queue<decimal> queue = new Queue<decimal>();
        private static decimal sum;

        public static decimal ComputeAverage(decimal value, int windowSize)
        {
            sum += value;
            queue.Enqueue(value);

            if (queue.Count > windowSize)
            {
                sum -= queue.Dequeue();
            }

            return sum / queue.Count;
        }

        /// <summary>
        /// SimpleMovingAverage
        /// </summary>
        /// <param name="candles"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static List<decimal> ComputeMovingAverage(Candle[] candles, int period)
        {
            var sma = new List<decimal>();

            if (candles.Length < period)
            {
                return new List<decimal>();
            }

            int counter = 1;
            foreach (var candle in candles)
            {
                if (counter >= period)
                {
                    var result = ComputeAverage(candle.Close, period);
                    sma.Add(result);
                }
                else
                {
                    sma.Add(0);
                }

                counter++;
            }

            return sma;
        }
    }
}
