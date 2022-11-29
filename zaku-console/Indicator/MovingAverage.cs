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
        /// <param name="windowSize"></param>
        /// <returns></returns>
        public static List<decimal> GetSimpleMovingAverage(Candle[] candles, int windowSize)
        {
            var sma = new List<decimal>();

            if (candles.Length < windowSize)
            {
                Console.WriteLine($"Number of data is less than required window-Size {windowSize}.");
                return new List<decimal>();
            }

            foreach (var candle in candles)
            {
                var ave = ComputeAverage(candle.Close, windowSize);
                sma.Add(ave);
            }

            return sma.Skip(windowSize - 1).ToList();
        }

        /// <summary>
        /// SimpleMovingAverage
        /// </summary>
        /// <param name="candles"></param>
        /// <param name="windowSize"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static decimal GetSimpleMovingAverage(Candle[] candles, int windowSize, int startIndex)
        {
            var ave = 0M;

            if (candles.Length < windowSize)
            {
                Console.WriteLine($"Number of data is less than required window-Size {windowSize}.");
                return ave;
            }

            var data = candles.Skip(startIndex).Take(windowSize).ToList();

            for (int i = 0; i < data.Count; i++)
            {
                ave = ComputeAverage(data[i].Close, windowSize);
            }

            return Math.Round(ave, 2);
        }

    }
}
